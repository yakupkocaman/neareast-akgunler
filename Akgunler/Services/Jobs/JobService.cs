using Akgunler.Data;
using Akgunler.Models.Jobs;
using Akgunler.ViewModels.Jobs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

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
                .Include(x => x.Staff).ThenInclude(x => x.Department)
                .Include(x => x.Tractor).ThenInclude(x => x.Make)
                .Include(x => x.Trailer).ThenInclude(x => x.Make)
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
                .Include(x => x.Staff).ThenInclude(x => x.Department)
                .Include(x => x.Tractor).ThenInclude(x => x.Make)
                .Include(x => x.Trailer).ThenInclude(x => x.VehicleType)
                .Include(x => x.Trailer).ThenInclude(x => x.Make)
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
                query = query.Where(x => x.TractorId == vehicleId);
            }

            if (status != "waiting" && staffId > 0)
            {
                query = query.Where(x => x.StaffId == staffId);
            }

            if (status != "waiting" && jobTypeId > 0)
            {
                query = query.Where(x => x.JobTypeId == jobTypeId);
            }

            if (status != "waiting" && !string.IsNullOrEmpty(search))
            {
                query = query.Where(x => (x.Sender.Name + " " + x.Sender.Title).Contains(search));
                query = query.Where(x => (x.Receiver.Name + " " + x.Receiver.Title).Contains(search));
                query = query.Where(x => (x.Staff.FirstName + " " + x.Staff.LastName).Contains(search));
                query = query.Where(x => (x.Tractor.RegistrationNo + " " + x.Tractor.Name).Contains(search));
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
                .Include(x => x.Staff).ThenInclude(x => x.Department)
                .Include(x => x.Tractor).ThenInclude(x => x.Make)
                .Include(x => x.Trailer).ThenInclude(x => x.VehicleType)
                .Include(x => x.Trailer).ThenInclude(x => x.Make)
                .Include(x => x.Freights).ThenInclude(x => x.ShippingDistrict).ThenInclude(x => x.Province)
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
                query = query.Where(x => x.TractorId == vehicleId);
            }

            if (status != "waiting" && staffId > 0)
            {
                query = query.Where(x => x.StaffId == staffId);
            }

            if (status != "waiting" && jobTypeId > 0)
            {
                query = query.Where(x => x.JobTypeId == jobTypeId);
            }

            if (status != "waiting" && !string.IsNullOrEmpty(search))
            {
                query = query.Where(x => (x.Sender.Name + " " + x.Sender.Title).Contains(search));
                query = query.Where(x => (x.Receiver.Name + " " + x.Receiver.Title).Contains(search));
                query = query.Where(x => (x.Staff.FirstName + " " + x.Staff.LastName).Contains(search));
                query = query.Where(x => (x.Tractor.RegistrationNo + " " + x.Tractor.Name).Contains(search));
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
                .Include(x => x.Staff).ThenInclude(x => x.Department)
                .Include(x => x.Tractor).ThenInclude(x => x.Make)
                .Include(x => x.Trailer).ThenInclude(x => x.VehicleType)
                .Include(x => x.Trailer).ThenInclude(x => x.Make)
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
