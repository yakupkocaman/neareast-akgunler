using ClosedXML.Attributes;

namespace Akgunler.ViewModels.Reports
{
	public class VehicleExpenseModel
    {

		[XLColumn(Header = "Plaka")]
		public string RegistrationNo { get; set; }


		[XLColumn(Header = "Model")]
		public string Model { get; set; }


		[XLColumn(Header = "Yıl")]
		public int ModelYear { get; set; }


		[XLColumn(Header = "Belge")]
		public int DocumentCount { get; set; }


		[XLColumn(Header = "Km")]
		public int Mileage { get; set; }


		[XLColumn(Header = "Eur")]
		public decimal TotalEur { get; set; }


		[XLColumn(Header = "Gbp")]
		public decimal TotalGbp { get; set; }


		[XLColumn(Header = "Try")]
		public decimal TotalTry { get; set; }


		[XLColumn(Header = "Usd")]
		public decimal TotalUsd { get; set; }

		[XLColumn(Header = "Durum", Ignore = true)]
		public bool IsActive { get; set; }
	}
}
