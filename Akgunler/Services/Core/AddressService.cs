using Akgunler.Data;
using Akgunler.Models.Core;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Akgunler.Services.Core
{
    public class AddressService : IAddressService
    {
        private readonly IRepository<Address> mAddressRepository;

        public AddressService(IRepository<Address> addressRepository)
        {
            mAddressRepository = addressRepository;
        }

        public void Insert(Address address) 
        {
            mAddressRepository.Add(address);
            mAddressRepository.SaveChange();
        }

        public void Update(Address address) 
        {
            mAddressRepository.Update(address);
        }

        public Address GetWithType(int addressId)
        {
            return mAddressRepository.Query()
                .Include(x => x.AddressType)
                .Where(x => x.Id == addressId && x.DeletedOn == null)
                .FirstOrDefault();
        }

        public List<Address> GetAllWithType(AddressGroup addressGroup)
        {
            return mAddressRepository.Query()
               .Include(x => x.AddressType)
               .Where(x => x.AddressType.AddressGroup == addressGroup && x.DeletedOn == null)
               .ToList();
        }

        public async Task<int> GetAddressIdAsync(string addressLine, int addressTypeId = 2)
        {
            var address = mAddressRepository.Query()
                .Where(x => x.AddressLine == addressLine && x.AddressTypeId == addressTypeId)
                .FirstOrDefault();

            if (address == null)
            {
                address = new Address { AddressLine = addressLine, AddressTypeId = addressTypeId, IsActive = true };
                mAddressRepository.Add(address);
                await mAddressRepository.SaveChangeAsync();
            }

            return address.Id;
        }

        public async Task<int> GetEmptyAddressId()
        {
            var address = await mAddressRepository.Query()
                .Where(x => x.IsActive && x.AddressLine == null)
                .FirstOrDefaultAsync();

            return address?.Id ?? 0;
        }
    }

    public interface IAddressService
    {
        void Insert(Address address);
        void Update(Address address);
        Address GetWithType(int addressId);
        List<Address> GetAllWithType(AddressGroup addressGroup);
        Task<int> GetAddressIdAsync(string addressLine, int addressTypeId = 2);
        Task<int> GetEmptyAddressId();

    }
}
