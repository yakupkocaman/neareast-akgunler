using Akgunler.Models.Core;
using System;
using System.Collections.Generic;

namespace Akgunler.ViewModels.Core
{
	public class DocumentModel
    {
		public int CustomerId { get; set; }
		public int VehicleId { get; set; }
		public int StaffId { get; set; }

		public int DocumentId { get; set; }
		public string FileName { get; set; }
		public int DocumentTypeId { get; set; }
		public int CurrencyId { get; set; }
		public DateTime Date { get; set; }
		public string Name { get; set; }
		public decimal Total { get; set; }
		public List<DocumentType> DocumentTypes { get; set; }
		public List<Currency> Currencies { get; set; }
	}
}
