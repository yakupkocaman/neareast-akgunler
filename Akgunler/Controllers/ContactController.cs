using Akgunler.Data;
using Akgunler.Models.Customers;
using Akgunler.ViewModels.Customers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Akgunler.Controllers
{
	[Route("api/[controller]/[action]")]
	public class ContactController : Controller
    {
        private readonly IRepository<Contact> mContactRepository;
        private readonly IRepository<CustomerContact> mCustomerContactRepository;

        public ContactController(
			IRepository<Contact> contactRepository,
			IRepository<CustomerContact> customerContactRepository
		)
		{
			mContactRepository = contactRepository;
			mCustomerContactRepository = customerContactRepository;
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromForm] ContactModel model)
		{
			var contact = new Contact();
			contact.FirstName = model.FirstName;
			contact.LastName = model.LastName;
			contact.Title = model.Title;
			contact.Phone1 = model.Phone1;
			contact.Phone2 = model.Phone2;
			contact.Email = model.Email;
			contact.Note = model.Note;
			contact.CreatedOn = DateTime.Now;
			
			mContactRepository.Add(contact);
			await mContactRepository.SaveChangeAsync();

			if (model.CustomerId > 0)
			{
				var customerContact = new CustomerContact { CustomerId = model.CustomerId, ContactId = contact.Id };
				mCustomerContactRepository.Add(customerContact);
				await mCustomerContactRepository.SaveChangeAsync();
			}
			
			return Ok();
		}


		[HttpPost("{id}")]
		public IActionResult Update(int id, [FromForm] ContactModel model)
		{
			var contact = mContactRepository.Query().FirstOrDefault(x => x.Id == id);

			if (contact == null)
			{
				return NotFound();
			}

			contact.FirstName = model.FirstName;
			contact.LastName = model.LastName;
			contact.Title = model.Title;
			contact.Phone1 = model.Phone1;
			contact.Phone2 = model.Phone2;
			contact.Email = model.Email;
			contact.Note = model.Note;
			contact.UpdatedOn = DateTime.Now;

			mContactRepository.Update(contact);

			return Ok();
		}
		
		[HttpPost("{id}")]
		public IActionResult Delete(int id)
		{
			var contact = mContactRepository.Query().FirstOrDefault(x => x.Id == id);

			if (contact == null)
			{
				return NotFound();
			}

			contact.DeletedOn = DateTime.Now;
			mContactRepository.Update(contact);

			return Ok();
		}
	}
}
