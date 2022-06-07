using Akgunler.Data;
using Akgunler.Models.Core;
using Akgunler.Models.Staffs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Akgunler.Services.Staffs
{
    public class StaffDocumentService : IStaffDocumentService
    {
        private readonly IRepository<StaffDocument> mStaffDocumentRepository;

        public StaffDocumentService(IRepository<StaffDocument> customerDocumentRepository)
        {
            mStaffDocumentRepository = customerDocumentRepository;
        }
        public StaffDocument GetByDocumentId(int documentId)
        {
            return mStaffDocumentRepository.Query()
                .Where(x => x.DocumentId == documentId)
                .AsNoTracking()
                .FirstOrDefault();
        }

        public List<Document> GetAllDocuments(int customerId)
        {
            return mStaffDocumentRepository.Query()
                .Include(x => x.Document).ThenInclude(x => x.DocumentType)
                .Include(x => x.Document).ThenInclude(x => x.Currency)
                .Where(x => x.StaffId == customerId)
                .Select(x => x.Document)
                .AsNoTracking()
                .ToList();
        }
    }

    public interface IStaffDocumentService
    {
        StaffDocument GetByDocumentId(int documentId);
        List<Document> GetAllDocuments(int customerId);
    }
}
