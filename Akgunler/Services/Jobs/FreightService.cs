using Akgunler.Data;
using Akgunler.Models.Jobs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Akgunler.Services.Jobs
{
    public class FreightService : IFreightService
    {
        private readonly IRepository<Freight> mFreightRepository;

        public FreightService(IRepository<Freight> freightRepository)
        {
            mFreightRepository = freightRepository;
        }

        public List<Freight> GetByJobId(int jobId)
        {
            return mFreightRepository.Query()
                .Include(x => x.ShippingDistrict).ThenInclude(x => x.Province).ThenInclude(x => x.Country)
                .Include(x => x.DeliveryDistrict).ThenInclude(x => x.Province).ThenInclude(x => x.Country)
                .Where(x => x.JobId == jobId && x.DeletedOn == null)
                .AsNoTracking()
                .ToList();
        }
    }

    public interface IFreightService
    {
        List<Freight> GetByJobId(int jobId);
    }
}
