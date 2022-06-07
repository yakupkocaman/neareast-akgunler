using ClosedXML.Attributes;
using System;

namespace Akgunler.ViewModels.Reports
{
	public class StaffJobModel
    {
		[XLColumn(Header = "Ad Soyad")]
		public string Name { get; set; }

		[XLColumn(Header = "Görev")]
		public int JobCount { get; set; }

		[XLColumn(Header = "Süre")]
		public TimeSpan Duration { get; set; }

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
