using Akgunler.Data;
using Akgunler.Models.Jobs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Akgunler.Services.Jobs
{
    public class JobStaffService : IJobStaffService
    {
        private readonly IRepository<JobStaff> mJobStaffRepository;

        public JobStaffService(IRepository<JobStaff> jobStaffRepository)
        {
            mJobStaffRepository = jobStaffRepository;
        }


        public List<JobStaff> GetByJobId(int jobId)
        {
            return mJobStaffRepository.Query()
                .Include(x => x.Staff)
                .Include(x => x.Staff).ThenInclude(x => x.Department)
                .Include(x => x.Tractor).ThenInclude(x => x.VehicleType)
                .Include(x => x.Tractor).ThenInclude(x => x.Color)
                .Include(x => x.Tractor).ThenInclude(x => x.Fuel)
                .Include(x => x.Tractor).ThenInclude(x => x.Make)
                .Include(x => x.Trailer).ThenInclude(x => x.VehicleType)
                .Include(x => x.Trailer).ThenInclude(x => x.Color)
                .Include(x => x.Trailer).ThenInclude(x => x.Fuel)
                .Include(x => x.Trailer).ThenInclude(x => x.Make)
                .Where(x => x.JobId == jobId)
                .AsNoTracking()
                .ToList();
        }
    }

    public interface IJobStaffService
    {
        List<JobStaff> GetByJobId(int jobId);
    }
}
