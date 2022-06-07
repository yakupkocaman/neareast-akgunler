using Akgunler.Data;
using Akgunler.Models.Core;
using Akgunler.Models.Vehicles;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Akgunler.Services.Vehicles
{
    public class VehicleDocumentService : IVehicleDocumentService
    {
        private readonly IRepository<VehicleDocument> mVehicleDocumentRepository;

        public VehicleDocumentService(IRepository<VehicleDocument> customerDocumentRepository)
        {
            mVehicleDocumentRepository = customerDocumentRepository;
        }
        public VehicleDocument GetByDocumentId(int documentId)
        {
            return mVehicleDocumentRepository.Query()
                .Where(x => x.DocumentId == documentId)
                .AsNoTracking()
                .FirstOrDefault();
        }

        public List<Document> GetAllDocuments(int customerId)
        {
            return mVehicleDocumentRepository.Query()
                .Include(x => x.Document).ThenInclude(x => x.DocumentType)
                .Include(x => x.Document).ThenInclude(x => x.Currency)
                .Where(x => x.VehicleId == customerId)
                .Select(x => x.Document)
                .AsNoTracking()
                .ToList();
        }
    }

    public interface IVehicleDocumentService
    {
        VehicleDocument GetByDocumentId(int documentId);
        List<Document> GetAllDocuments(int customerId);
    }
}
