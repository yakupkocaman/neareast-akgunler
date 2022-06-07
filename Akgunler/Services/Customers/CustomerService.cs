using Akgunler.Data;
using Akgunler.Models.Customers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Akgunler.Services.Customers
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> mCustomerRepository;

        public CustomerService(
            IRepository<Customer> customerRepository
        )
        {
            mCustomerRepository = customerRepository;
        }

        public void Insert(Customer customer) 
        {
            mCustomerRepository.Add(customer);
            mCustomerRepository.SaveChange();
        }

        public void Update(Customer customer) 
        {
            mCustomerRepository.Update(customer);
        }

        public Customer GetWithGroup(int customerId)
        {
            return mCustomerRepository.Query()
                .Include(x => x.Group)
                .Where(x => x.Id == customerId && x.DeletedOn == null)
                .FirstOrDefault();
        }

        public List<Customer> GetAllWithGroup()
        {
            return mCustomerRepository.Query()
                  .Include(x => x.Group)
                  .Where(x => x.DeletedOn == null)
                  .ToList();
        }
    }

    public interface ICustomerService
    {
        void Insert(Customer customer);
        void Update(Customer customer);
        Customer GetWithGroup(int customerId);
        List<Customer> GetAllWithGroup();
    }
}
