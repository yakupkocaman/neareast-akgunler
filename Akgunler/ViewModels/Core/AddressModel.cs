using Akgunler.Models.Core;
using System.Collections.Generic;

namespace Akgunler.ViewModels.Core
{
	public class AddressModel
	{
		public string Name { get; set; }
		public int CustomerId { get; set; }
		public int AddressId { get; set; }
		public int AddressTypeId { get; set; }
		public string AddressLine { get; set; }
        public int? DistrictId { get; set; }
        public string District { get; set; }
        public int? ProvinceId { get; set; }
        public string Province { get; set; }
        public string CountryId { get; set; }
        public string Country { get; set; }
		public string PostalCode { get; set; }
		public string Note { get; set; }
		public bool IsActive { get; set; }
		public List<AddressType> AddressTypes { get; set; }
	}
}
