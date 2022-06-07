using System.Collections.Generic;

namespace Akgunler.ViewModels.Jobs
{
	public class FreightModel
    {
		public int FreightId { get; set; }
		public int JobId { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string ShippingAddressLine { get; set; }
        public int? ShippingDistrictId { get; set; }
        public string ShippingDistrict { get; set; }
        public int? ShippingProvinceId { get; set; }
        public string ShippingProvince { get; set; }
        public string ShippingCountryId { get; set; }
        public string ShippingCountry { get; set; }
        public string DeliveryAddressLine { get; set; }
        public int? DeliveryDistrictId { get; set; }
        public string DeliveryDistrict { get; set; }
        public int? DeliveryProvinceId { get; set; }
        public string DeliveryProvince { get; set; }
        public string DeliveryCountryId { get; set; }
        public string DeliveryCountry { get; set; }
        public bool IsDomesticShipping { get; set; }
        public decimal DomesticShippingPrice { get; set; }
        public string Note { get; set; }
    }
}
