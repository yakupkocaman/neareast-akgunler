using Akgunler.Data;
using Akgunler.Models.Jobs;
using Akgunler.ViewModels.Jobs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Akgunler.Controllers
{
	[Route("api/[controller]/[action]")]
	public class FreightController : Controller
    {
        private readonly IRepository<Freight> mFreightRepository;

        public FreightController(
			IRepository<Freight> freightRepository
		)
		{
			mFreightRepository = freightRepository;
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromForm] FreightModel model)
		{
			var freight = new Freight();
			freight.JobId = model.JobId;
			freight.Title = model.Title;
			freight.Type = model.Type;
			freight.ShippingAddressLine = model.ShippingAddressLine;
			freight.ShippingDistrictId = model.ShippingDistrictId;
			freight.DeliveryAddressLine = model.DeliveryAddressLine;
			freight.DeliveryDistrictId = model.DeliveryDistrictId;
			freight.IsDomesticShipping = model.IsDomesticShipping;
			freight.DomesticShippingPrice = model.DomesticShippingPrice;
			freight.Note = model.Note;
			freight.CreatedOn = DateTime.Now;

			mFreightRepository.Add(freight);
			await mFreightRepository.SaveChangeAsync();

			return Ok();
		}


		[HttpPost("{id}")]
		public IActionResult Update(int id, [FromForm] FreightModel model)
		{
			var freight = mFreightRepository.Query()
				.FirstOrDefault(x => x.Id == id);

			if (freight == null)
			{
				return NotFound();
			}

			freight.Title = model.Title;
			freight.Type = model.Type;
			freight.ShippingAddressLine = model.ShippingAddressLine;
			freight.ShippingDistrictId = model.ShippingDistrictId;
			freight.DeliveryAddressLine = model.DeliveryAddressLine;
			freight.DeliveryDistrictId = model.DeliveryDistrictId;
			freight.IsDomesticShipping = model.IsDomesticShipping;
			freight.DomesticShippingPrice = model.DomesticShippingPrice;
			freight.Note = model.Note;
			freight.UpdatedOn = DateTime.Now;

			mFreightRepository.Update(freight);

			return Ok();
		}
		
		[HttpPost("{id}")]
		public IActionResult Delete(int id)
		{
			var freight = mFreightRepository.Query()
				.FirstOrDefault(x => x.Id == id);

			if (freight == null)
			{
				return NotFound();
			}

			freight.DeletedOn = DateTime.Now;
			mFreightRepository.Update(freight);

			return Ok();
		}
	}
}
