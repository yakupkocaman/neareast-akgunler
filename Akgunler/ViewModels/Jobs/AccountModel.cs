using Akgunler.Models.Core;
using Akgunler.Models.Jobs;
using System;
using System.Collections.Generic;

namespace Akgunler.ViewModels.Jobs
{
	public class AccountModel
    {
		public int AccountId { get; set; }
		public int JobId { get; set; }
		public int AccountTypeId { get; set; }
		public int CurrencyId { get; set; }
		public decimal Debit { get; set; }
		public decimal Credit { get; set; }
		public DateTime Date { get; set; }
		public string Note { get; set; }


		public List<AccountType> AccountTypes { get; set; }
		public List<Currency> Currencies { get; set; }
	}
}
