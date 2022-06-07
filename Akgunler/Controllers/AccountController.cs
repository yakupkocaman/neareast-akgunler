using Akgunler.Data;
using Akgunler.Models.Jobs;
using Akgunler.ViewModels.Jobs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Akgunler.Controllers
{
	[Route("api/[controller]/[action]")]
	public class AccountController : Controller
    {
        private readonly IRepository<Account> mAccountRepository;

        public AccountController(
			IRepository<Account> accountRepository  
		)
		{
			mAccountRepository = accountRepository;
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromForm] AccountModel model)
		{
			var account = new Account();
			account.JobId = model.JobId;
			account.AccountTypeId = model.AccountTypeId;
			account.CurrencyId = model.CurrencyId;
			account.Debit = model.Debit;
			account.Credit = model.Credit;
			account.Date = model.Date;
			account.Note = model.Note;
			account.CreatedOn = DateTime.Now;
			
			mAccountRepository.Add(account);
			await mAccountRepository.SaveChangeAsync();

			return Ok();
		}


		[HttpPost("{id}")]
		public IActionResult Update(int id, [FromForm] AccountModel model)
		{
			var account = mAccountRepository.Query().FirstOrDefault(x => x.Id == id);

			if (account == null)
			{
				return NotFound();
			}

			account.AccountTypeId = model.AccountTypeId;
			account.CurrencyId = model.CurrencyId;
			account.Debit = model.Debit;
			account.Credit = model.Credit;
			account.Date = model.Date;
			account.Note = model.Note;
			account.UpdatedOn = DateTime.Now;
			
			mAccountRepository.Update(account);

			return Ok();
		}

		[HttpPost("{id}")]
		public IActionResult Delete(int id)
		{
			var account = mAccountRepository.Query().FirstOrDefault(x => x.Id == id);

			if (account == null)
			{
				return NotFound();
			}

			account.DeletedOn = DateTime.Now;
			mAccountRepository.Update(account);

			return Ok();
		}
	}
}
