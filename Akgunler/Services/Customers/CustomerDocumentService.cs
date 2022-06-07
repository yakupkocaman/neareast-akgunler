using Akgunler.Data;
using Akgunler.Models.Core;
using Akgunler.Models.Customers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Akgunler.Services.Customers
{
    public class CustomerDocumentService : ICustomerDocumentService
    {
        private readonly IRepository<CustomerDocument> mCustomerDocumentRepository;

        public CustomerDocumentService(IRepository<CustomerDocument> customerDocumentRepository)
        {
            mCustomerDocumentRepository = customerDocumentRepository;
        }
        public CustomerDocument GetByDocumentId(int documentId)
        {
            return mCustomerDocumentRepository.Query()
                .Where(x => x.DocumentId == documentId)
                .AsNoTracking()
                .FirstOrDefault();
        }

        public List<Document> GetAllDocuments(int customerId)
        {
            return mCustomerDocumentRepository.Query()
                .Include(x => x.Document).ThenInclude(x => x.DocumentType)
                .Include(x => x.Document).ThenInclude(x => x.Currency)
                .Where(x => x.CustomerId == customerId)
                .Select(x => x.Document)
                .AsNoTracking()
                .ToList();
        }
    }

    public interface ICustomerDocumentService
    {
        CustomerDocument GetByDocumentId(int documentId);
        List<Document> GetAllDocuments(int customerId);
    }
}
