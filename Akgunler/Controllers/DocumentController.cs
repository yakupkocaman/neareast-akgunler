using Akgunler.Data;
using Akgunler.Models.Core;
using Akgunler.Models.Customers;
using Akgunler.Models.Staffs;
using Akgunler.Models.Vehicles;
using Akgunler.ViewModels.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Akgunler.Controllers
{
	[Route("api/[controller]/[action]")]
	public class DocumentController : Controller
	{
		public readonly IWebHostEnvironment mHostingEnvironment;
        private readonly IRepository<Document> mDocumentRepository;
        private readonly IRepository<CustomerDocument> mCustomerDocumentRepository;
        private readonly IRepository<StaffDocument> mStaffDocumentRepository;
        private readonly IRepository<VehicleDocument> mVehicleDocumentRepository;

        public DocumentController(
			IWebHostEnvironment hostingEnvironment,
			IRepository<Document> documentRepository,
			IRepository<CustomerDocument> customerDocumentRepository,
			IRepository<StaffDocument> staffDocumentRepository,
			IRepository<VehicleDocument> vehicleDocumentRepository
		){
			mHostingEnvironment = hostingEnvironment;
			mDocumentRepository = documentRepository;
			mCustomerDocumentRepository = customerDocumentRepository; 
			mStaffDocumentRepository = staffDocumentRepository; 
			mVehicleDocumentRepository = vehicleDocumentRepository; 
		}

		[HttpPost]
		public async Task<ActionResult> Upload()
		{
			var file = Request.Form.Files["document"];
			var extension = Path.GetExtension(file.FileName);
			var uploadDir = Path.Combine(mHostingEnvironment.WebRootPath, "Uploads");

			if (!Directory.Exists(uploadDir))
			{
				Directory.CreateDirectory(uploadDir);
			}

			try
			{
				if (file.Length > 0)
				{
					string fileName = Guid.NewGuid().ToString() + extension;
					var path = Path.Combine(uploadDir, fileName);

					using (var fileStream = System.IO.File.Create(path))
					{
						file.CopyTo(fileStream);
					}

					var document = new Document
					{
						FileName = fileName,
						FileType = extension.Substring(1).ToLower(),
						CreatedOn = DateTime.Now,
						Date = DateTime.Now,
					};

					mDocumentRepository.Add(document);
					await mDocumentRepository.SaveChangeAsync();

					return Ok(document);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				return BadRequest();
			}

			return BadRequest();
		}


		[HttpPost("{id}")]
		public async Task<IActionResult> Update(int id, [FromForm] DocumentModel model)
		{
			var document = mDocumentRepository.Query()
				.FirstOrDefault(x => x.Id == id);

			if (document == null)
			{
				return NotFound();
			}

			document.DocumentTypeId = model.DocumentTypeId;
			document.CurrencyId = model.CurrencyId;
			document.Name = model.Name;
			document.Date = model.Date;
			document.Total = model.Total;
			document.UpdatedOn = DateTime.Now;

			mDocumentRepository.Update(document);

			if (model.CustomerId > 0)
			{
				var customerDocument = mCustomerDocumentRepository.Query()
					.FirstOrDefault(x => x.DocumentId == document.Id);

				if (customerDocument == null)
				{
					customerDocument = new CustomerDocument { CustomerId = model.CustomerId, DocumentId = document.Id };
					mCustomerDocumentRepository.Add(customerDocument);
					await mCustomerDocumentRepository.SaveChangeAsync();
				}
			}

			if (model.VehicleId > 0)
			{
				var vehicleDocument = mVehicleDocumentRepository.Query()
					.FirstOrDefault(x => x.Id == document.Id);

				if (vehicleDocument == null)
				{
					vehicleDocument = new VehicleDocument { VehicleId = model.VehicleId, DocumentId = document.Id };
					mVehicleDocumentRepository.Add(vehicleDocument);
					await mVehicleDocumentRepository.SaveChangeAsync();
				}
			}

			if (model.StaffId > 0)
			{
				var staffDocument = mStaffDocumentRepository.Query()
					.FirstOrDefault(x => x.Id == document.Id);

				if (staffDocument == null)
				{
					staffDocument = new StaffDocument { StaffId = model.StaffId, DocumentId = document.Id };
					mStaffDocumentRepository.Add(staffDocument);
					await mStaffDocumentRepository.SaveChangeAsync();
				}
			}

			return Ok();
		}

		[HttpPost("{id}")]
		public IActionResult Delete(int id)
		{
			var document = mDocumentRepository.Query()
				.FirstOrDefault(x => x.Id == id);

			if (document == null)
			{
				return NotFound();
			}

			/*
			var customerDocument = CustomerDocument.GetByDocumentId(id);
			if (customerDocument != null)
			{
				CustomerDocument.Delete(customerDocument);
			}
			
			var vehicleDocument = VehicleDocument.GetByDocumentId(id);
			if (vehicleDocument != null)
			{
				VehicleDocument.Delete(vehicleDocument);
			}
			
			var staffDocument = StaffDocument.GetByDocumentId(id);
			if (staffDocument != null)
			{
				StaffDocument.Delete(staffDocument);
			}
			*/

			document.DeletedOn = DateTime.Now;
			mDocumentRepository.Update(document);

			return Ok();
		}
	}
}
