using ClosedXML.Attributes;

namespace Akgunler.ViewModels.Reports
{
	public class CustomerBalanceModel
    {
		[XLColumn(Header = "Ünvan")]
		public string Title { get; set; }

		[XLColumn(Header = "Görev")]
		public int JobCount { get; set; }

		[XLColumn(Header = "Borç")]
		public decimal TotalDebit { get; set; }

		[XLColumn(Header = "Alacak")]
		public decimal TotalCredit { get; set; }

		[XLColumn(Header = "Bakiye")]
		public decimal TotalBalance { get; set; }

		[XLColumn(Header = "Birim")]
		public string Currency { get; set; }

		[XLColumn(Header = "Durum", Ignore = true)]
		public bool IsActive { get; set; }
	}
}
