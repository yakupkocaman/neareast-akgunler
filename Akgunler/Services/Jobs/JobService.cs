using Akgunler.Data;
using Akgunler.Models.Core;
using Akgunler.Models.Customers;
using Akgunler.Models.Jobs;
using Akgunler.ViewModels.Jobs;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Akgunler.Services.Jobs
{
    public class JobService : IJobService
    {
        private readonly IRepository<Job> mJobRepository;

        public JobService(IRepository<Job> jobRepository)
        {
            mJobRepository = jobRepository;
        }
        
        public void Insert(Job job) 
        {
            mJobRepository.Add(job);
            mJobRepository.SaveChange();
        }

        public void Update(Job job) 
        {
            mJobRepository.Update(job);
        }
        
        public bool CheckVersion(int jobId, byte[] jobVersion)
        {
            var currentVersion = mJobRepository.Query()
                .Where(x => x.Id == jobId)
                .Select(x => x.RowVersion)
                .First();

            return BitConverter.ToInt64(currentVersion.Reverse().ToArray(), 0) <= BitConverter.ToInt64(jobVersion.Reverse().ToArray(), 0);
        }

        public JobCountModel GetJobCounts()
        {
            var sql = @"DECLARE @startDate DATETIME = DATEADD(SECOND, 1, DATEADD(DAY, DATEDIFF(DAY, 0, GETDATE()), 0))
						DECLARE @endDate DATETIME = DATEADD(SECOND, -1, DATEADD(DAY, DATEDIFF(DAY, 0, GETDATE())+1, 0))

						SELECT 
							SUM( CASE WHEN HasUpdate = 1 AND JJ.DeletedOn IS NULL AND JJ.IsCancelled = 0 THEN 1 END) WaitingCount,
							SUM( CASE WHEN IsActive = 1 AND StartDate BETWEEN @startDate AND @endDate AND JJ.DeletedOn IS NULL THEN 1 END) TodayCount,
							SUM( CASE WHEN IsActive = 1 AND HasUpdate = 0 AND JJ.DeletedOn IS NULL AND JJ.IsCancelled = 0 THEN 1 END) ActiveCount,
							SUM( CASE WHEN IsActive = 0 AND JJ.DeletedOn IS NULL AND JJ.HasUpdate = 0 AND JJ.IsCancelled = 0 THEN 1 END) CompletedCount,
							SUM( CASE WHEN JJ.DeletedOn IS NULL THEN 1 END) AllCount
						FROM Job.Job JJ";

            return Job.One<JobCountModel>(sql);
        }

        public Job GetFull(int jobId)
        {
            return mJobRepository.Query()
                .Include(x => x.Sender)
                .Include(x => x.Receiver)
                .Include(x => x.DeparturePort)
                .Include(x => x.ArrivalPort)
                .Include(x => x.DepartureShip)
                .Include(x => x.ArrivalShip)
                .Include(x => x.JobType)
                .Where(x => x.Id == jobId && x.DeletedOn == null)
                .FirstOrDefault();
        }

        public (int, List<Job>) GetPagedFull(int page, int count, string status, string search, int vehicleId = -1, int staffId = -1, int jobTypeId = -1, DateTime? start = null, DateTime? end = null)
        {
            var query = mJobRepository.Query()
                .Include(x => x.Sender)
                .Include(x => x.Receiver)
                .Include(x => x.DeparturePort)
                .Include(x => x.ArrivalPort)
                .Include(x => x.DepartureShip)
                .Include(x => x.ArrivalShip)
                .Include(x => x.JobType)
                .Include(x => x.JobStaffs).ThenInclude(x => x.Staff)
                .Include(x => x.JobStaffs).ThenInclude(x => x.Tractor).ThenInclude(x => x.Make)
                .Include(x => x.JobStaffs).ThenInclude(x => x.Trailer).ThenInclude(x => x.Make)
                .Where(x => x.DeletedOn == null)
                .AsNoTracking();

            if (status != "waiting" && start.HasValue)
            {
                start = start.Value.Date;
                query = query.Where(x => x.StartDate >= start);
            }

            if (status != "waiting" && end.HasValue)
            {
                end = end.Value.Date.AddDays(1).AddTicks(-1);
                query = query.Where(x => x.StartDate<= end);
            }

            if (status != "waiting" && vehicleId > 0)
            {
                query = query.Where(x => x.JobStaffs.Any(x => x.TractorId == vehicleId));
            }

            if (status != "waiting" && staffId > 0)
            {
                query = query.Where(x => x.JobStaffs.Any(x => x.StaffId == staffId));
            }

            if (status != "waiting" && jobTypeId > 0)
            {
                query = query.Where(x => x.JobTypeId == jobTypeId);
            }

            if (status != "waiting" && !string.IsNullOrEmpty(search))
            {
                query = query.Where(x => (x.Sender.Name + " " + x.Sender.Title).Contains(search));
                query = query.Where(x => (x.Receiver.Name + " " + x.Receiver.Title).Contains(search));
                query = query.Where(x => x.JobStaffs.Any(s => (s.Staff.FirstName + " " + s.Staff.LastName).Contains(search)));
                query = query.Where(x => x.JobStaffs.Any(s => (s.Tractor.RegistrationNo + " " + s.Tractor.Name).Contains(search)));
            }

            if (status == "waiting")
            {
                query = query.Where(x => x.HasUpdate && !x.IsCancelled);
            }
            else if (status == "active")
            {
                query = query.Where(x => x.IsActive && !x.HasUpdate && !x.IsCancelled);
            }
            else if (status == "today")
            {
                start = DateTime.Now.Date;
                end = DateTime.Now.Date.AddDays(1).AddTicks(-1);

                query = query.Where(x => x.IsActive && x.StartDate >= start && x.StartDate <= end);
            }
            else if (status == "completed")
            {
                query = query.Where(x => !x.IsActive && !x.HasUpdate && !x.IsCancelled);
            }

            var totalCount = query.Count();

            var result = query
                .OrderBy(x => x.Id)
                .Skip((page - 1) * count).Take(count)
                .ToList();

            return (totalCount, result);
        }

        public IQueryable<Job> GetFullQueryable(string status, string search, int vehicleId = -1, int staffId = -1, int jobTypeId = -1, DateTime? start = null, DateTime? end = null)
        {
            var query = mJobRepository.Query()
                .Include(x => x.Sender)
                .Include(x => x.Receiver)
                .Include(x => x.DeparturePort)
                .Include(x => x.ArrivalPort)
                .Include(x => x.DepartureShip)
                .Include(x => x.ArrivalShip)
                .Include(x => x.JobType)
                .Include(x => x.JobStaffs).ThenInclude(x => x.Staff)
                .Include(x => x.JobStaffs).ThenInclude(x => x.Tractor).ThenInclude(x => x.Make)
                .Include(x => x.JobStaffs).ThenInclude(x => x.Trailer).ThenInclude(x => x.Make)
                .Where(x => x.DeletedOn == null)
                .AsNoTracking();

            if (status != "waiting" && start.HasValue)
            {
                start = start.Value.Date;
                query = query.Where(x => x.StartDate >= start);
            }

            if (status != "waiting" && end.HasValue)
            {
                end = end.Value.Date.AddDays(1).AddTicks(-1);
                query = query.Where(x => x.StartDate <= end);
            }

            if (status != "waiting" && vehicleId > 0)
            {
                query = query.Where(x => x.JobStaffs.Any(x => x.TractorId == vehicleId));
            }

            if (status != "waiting" && staffId > 0)
            {
                query = query.Where(x => x.JobStaffs.Any(x => x.StaffId == staffId));
            }

            if (status != "waiting" && jobTypeId > 0)
            {
                query = query.Where(x => x.JobTypeId == jobTypeId);
            }

            if (status != "waiting" && !string.IsNullOrEmpty(search))
            {
                query = query.Where(x => (x.Sender.Name + " " + x.Sender.Title).Contains(search));
                query = query.Where(x => (x.Receiver.Name + " " + x.Receiver.Title).Contains(search));
                query = query.Where(x => x.JobStaffs.Any(s => (s.Staff.FirstName + " " + s.Staff.LastName).Contains(search)));
                query = query.Where(x => x.JobStaffs.Any(s => (s.Tractor.RegistrationNo + " " + s.Tractor.Name).Contains(search)));
            }

            if (status == "waiting")
            {
                query = query.Where(x => x.HasUpdate && !x.IsCancelled);
            }
            else if (status == "active")
            {
                query = query.Where(x => x.IsActive && !x.HasUpdate && !x.IsCancelled);
            }
            else if (status == "today")
            {
                start = DateTime.Now.Date;
                end = DateTime.Now.Date.AddDays(1).AddTicks(-1);

                query = query.Where(x => x.IsActive && x.StartDate >= start && x.StartDate <= end);
            }
            else if (status == "completed")
            {
                query = query.Where(x => !x.IsActive && !x.HasUpdate && !x.IsCancelled);
            }

            return query;
        }

        public List<Job> GetAllFull(string q = "all")
        {
            var query = mJobRepository.Query()
               .Include(x => x.Sender)
               .Include(x => x.Receiver)
                .Include(x => x.DeparturePort)
                .Include(x => x.ArrivalPort)
                .Include(x => x.DepartureShip)
                .Include(x => x.ArrivalShip)
               .Include(x => x.JobType)
               .Include(x => x.JobStaffs).ThenInclude(x => x.Staff)
               .Include(x => x.JobStaffs).ThenInclude(x => x.Tractor).ThenInclude(x => x.Make)
               .Where(x => x.DeletedOn == null)
               .AsNoTracking();

            if (q == "waiting")
            {
                query = query.Where(x => x.HasUpdate && !x.IsCancelled);
            }
            else if (q == "active")
            {
                query = query.Where(x => x.IsActive && !x.HasUpdate && !x.IsCancelled);
            }
            else if (q == "today")
            {
                var start = DateTime.Now.Date;
                var end = DateTime.Now.Date.AddDays(1).AddTicks(-1);

                query = query.Where(x => x.IsActive && x.StartDate >= start && x.StartDate <= end);
            }
            else if (q == "completed")
            {
                query = query.Where(x => x.IsActive && !x.HasUpdate && !x.IsCancelled);
            }

            var result = query
                .OrderBy(x => x.Id)
                .ToList();

            return result;
        }

        /*
        public VehicleJobModel GetVehicleJobs(DateTime startDate, DateTime endDate, JobReportStatus reportStatus)
        {
            var sql = @"SELECT 
							RegistrationNo, Model, ModelYear,
							COUNT(VehicleId) JobCount,
							SUM(Duration) Duration,
							SUM(Mileage) Mileage,
							SUM(TotalGbp) TotalGbp,
							SUM(TotalUsd) TotalUsd,
							SUM(TotalEur) TotalEur,
							SUM(TotalTry) TotalTry
						FROM
						(
							SELECT 
								VV.VehicleId,
								VV.RegistrationNo,
								VV.Model,
								VV.ModelYear,
								DATEDIFF(SECOND, JJ.StartDate, JJ.FinishDate) Duration,
								(JJS.FinishVehicleMileage - JJS.StartVehicleMileage) Mileage,
								(SELECT SUM(JA.Debit - JA.Credit) FROM Job.Account JA WHERE JA.JobId = JJ.JobId AND JA.CurrencyId = 1) TotalGbp,
								(SELECT SUM(JA.Debit - JA.Credit) FROM Job.Account JA WHERE JA.JobId = JJ.JobId AND JA.CurrencyId = 2) TotalUsd,
								(SELECT SUM(JA.Debit - JA.Credit) FROM Job.Account JA WHERE JA.JobId = JJ.JobId AND JA.CurrencyId = 3) TotalEur,
								(SELECT SUM(JA.Debit - JA.Credit) FROM Job.Account JA WHERE JA.JobId = JJ.JobId AND JA.CurrencyId = 4) TotalTry
							FROM Job.JobStaff JJS
							LEFT JOIN Job.Job JJ ON JJS.JobId = JJ.JobId
							LEFT JOIN Vehicle.Vehicle VV ON JJS.VehicleId = VV.VehicleId
							LEFT JOIN Staff.Staff SS ON JJS.StaffId = SS.StaffId
							WHERE 
								JJ.JobTypeId = 4
								@reportStatusCondition
						) JJSQ
						GROUP BY JJSQ.VehicleId, RegistrationNo,  Model, ModelYear";

            string reportStatusCondition = "";

            if (reportStatus == JobReportStatus.Waiting)
            {
                reportStatusCondition = "AND JJ.StartDate > @startDate";
            }
            else if (reportStatus == JobReportStatus.Started)
            {
                reportStatusCondition = "AND JJ.StartDate <= @startDate AND JJ.FinishDate > @endDate";
            }
            else if (reportStatus == JobReportStatus.Finished)
            {
                reportStatusCondition = "AND JJ.FinishDate < @endDate";
            }

            return One<VehicleJobModel>(sql, new { reportStatusCondition, startDate, endDate });
        }
		*/

    }

    public interface IJobService
    {
        void Insert(Job job);
        void Update(Job job);
        bool CheckVersion(int jobId, byte[] jobVersion);
        JobCountModel GetJobCounts();
        Job GetFull(int jobId);
        (int, List<Job>) GetPagedFull(int page, int count, string status, string search, int vehicleId = -1, int staffId = -1, int jobTypeId = -1, DateTime? start = null, DateTime? end = null);
        IQueryable<Job> GetFullQueryable(string status, string search, int vehicleId = -1, int staffId = -1, int jobTypeId = -1, DateTime? start = null, DateTime? end = null);
    }
}
