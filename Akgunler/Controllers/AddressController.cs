using Akgunler.Data;
using Akgunler.Models.Core;
using Akgunler.Models.Customers;
using Akgunler.ViewModels.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Akgunler.Controllers
{
	[Route("api/[controller]/[action]")]
	public class AddressController : Controller
    {
        private readonly IRepositoryWithTypedId<Country, string> mCountryRepository;
		private readonly IRepository<Province> mProvinceRepository;
		private readonly IRepository<District> mDistrictRepository;
		private readonly IRepository<Address> mAddressRepository;
        private readonly IRepository<CustomerAddress> mCustomerAddressRepository;

        public AddressController(
			IRepositoryWithTypedId<Country, string> countryRepository,
			IRepository<Province> provinceRepository,
			IRepository<District> districtRepository,
			IRepository<Address> addressRepository,
			IRepository<CustomerAddress> customerAddressRepository
		)
        {
			mCountryRepository = countryRepository;
			mProvinceRepository = provinceRepository;
			mDistrictRepository = districtRepository;
			mAddressRepository = addressRepository;
			mCustomerAddressRepository = customerAddressRepository; 
		}

		[HttpGet]
		public IActionResult Countries()
		{
			var countries = mCountryRepository.Query().OrderBy(x => x.Name).ToList();

			return Ok(countries);
		}

		[HttpGet]
		public IActionResult CountriesLocal()
		{
			var countries = mCountryRepository.Query().Where(x => x.Id == "TR" || x.Id == "CT").OrderBy(x => x.Name).ToList();

			return Ok(countries);
		}

		[HttpGet]
		public IActionResult Provinces(string countryId)
		{
			var provinces = mProvinceRepository.Query().Where(x => x.CountryId == countryId).OrderBy(x => x.Name).ToList();
			
			return Ok(provinces);
		}

		[HttpGet]
		public IActionResult Districts(int provinceId)
		{
			var cities = mDistrictRepository.Query().Where(x => x.ProvinceId == provinceId).OrderBy(x => x.Name).ToList();

			return Ok(cities);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromForm] AddressModel model)
		{
			var address = new Address();
			address.Name = model.Name;
			address.AddressTypeId = model.AddressTypeId;
			address.AddressLine = model.AddressLine;
			address.DistrictId = model.DistrictId;
			address.PostalCode = model.PostalCode;
			address.Note = model.Note;
			address.IsActive = model.IsActive;
			address.CreatedOn = DateTime.Now;

			mAddressRepository.Add(address);
			await mAddressRepository.SaveChangeAsync();

			if (model.CustomerId > 0)
			{
				var customerAddress = new CustomerAddress { CustomerId = model.CustomerId, AddressId = address.Id };
				mCustomerAddressRepository.Add(customerAddress);
				await mCustomerAddressRepository.SaveChangeAsync();
			}

			return Ok();
		}


		[HttpPost("{id}")]
		public IActionResult Update(int id, [FromForm] AddressModel model)
		{
			var address = mAddressRepository.Query().FirstOrDefault(x => x.Id == id);

			if (address == null)
			{
				return NotFound();
			}

			address.Name = model.Name;
			address.AddressTypeId = model.AddressTypeId;
			address.AddressLine = model.AddressLine;
			address.DistrictId = model.DistrictId;
			address.PostalCode = model.PostalCode;
			address.Note = model.Note;
			address.IsActive = model.IsActive;
			address.UpdatedOn = DateTime.Now;

			mAddressRepository.Update(address);

			return Ok();
		}

		[HttpPost("{id}")]
		public IActionResult Delete(int id)
		{
			var address = mAddressRepository.Query().FirstOrDefault(x => x.Id == id);

			if (address == null)
			{
				return NotFound();
			}

			address.DeletedOn = DateTime.Now;
			mAddressRepository.Update(address);

			return Ok();
		}
	}
}
