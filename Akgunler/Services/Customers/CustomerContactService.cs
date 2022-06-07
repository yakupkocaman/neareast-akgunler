using Akgunler.Data;
using Akgunler.Models.Customers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Akgunler.Services.Customers
{
    public class CustomerContactService : ICustomerContactService
    {
        private readonly IRepository<CustomerContact> mCustomerContactRepository;

        public CustomerContactService(
            IRepository<CustomerContact> customerContactRepository
        )
        {
            mCustomerContactRepository = customerContactRepository;
        }
        public CustomerContact GetByContactId(int contactId)
        {
            return mCustomerContactRepository.Query()
                .Where(x => x.ContactId == contactId)
                .FirstOrDefault();
        }

        public List<Contact> GetAllContacts(int customerId)
        {
            return mCustomerContactRepository.Query()
                .Include(x => x.Contact)
                .Where(x => x.CustomerId == customerId)
                .Select(x => x.Contact)
                .ToList();
        }
    }

    public interface ICustomerContactService
    {
        CustomerContact GetByContactId(int contactId);
        List<Contact> GetAllContacts(int customerId);
    }
}
