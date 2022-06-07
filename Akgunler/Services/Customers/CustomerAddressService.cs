using Akgunler.Data;
using Akgunler.Models.Core;
using Akgunler.Models.Customers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Akgunler.Services.Customers
{
    public class CustomerAddressService : ICustomerAddressService
    {
        private readonly IRepository<CustomerAddress> mCustomerAddressRepository;

        public CustomerAddressService(
            IRepository<CustomerAddress> customerAddressRepository
        )
        {
            mCustomerAddressRepository = customerAddressRepository;
        }

        public CustomerAddress GetByAddressId(int addressId)
        {
            return mCustomerAddressRepository.Query()
                .Include(x => x.Address).ThenInclude(x => x.District).ThenInclude(x => x.Province).ThenInclude(x => x.Country)
                .Where(x => x.AddressId == addressId)
                .FirstOrDefault();
        }

        public List<Address> GetAllAddresses(int customerId)
        {
            return mCustomerAddressRepository.Query()
                .Include(x => x.Address).ThenInclude(x => x.AddressType)
                .Include(x => x.Address).ThenInclude(x => x.District).ThenInclude(x => x.Province).ThenInclude(x => x.Country)
                .Where(x => x.CustomerId == customerId)
                .Select(x => x.Address)
                .ToList();
        }
    }

    public interface ICustomerAddressService
    {
        CustomerAddress GetByAddressId(int addressId);
        List<Address> GetAllAddresses(int customerId);
    }
}
