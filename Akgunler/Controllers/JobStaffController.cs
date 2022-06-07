using System.Linq;
using System.Threading.Tasks;
using Akgunler.Data;
using Akgunler.Models.Jobs;
using Akgunler.ViewModels.Jobs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Akgunler.Controllers
{
	[Route("api/[controller]/[action]")]
	public class JobStaffController : Controller
    {
        private readonly IRepository<JobStaff> mJobStaffRepository;

        public JobStaffController(
			IRepository<JobStaff> jobStaffRepository
		)
		{
			mJobStaffRepository = jobStaffRepository;
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromForm] JobStaffModel model)
		{
			var jobStaff = new JobStaff();
			jobStaff.JobId = model.JobId;
			jobStaff.StaffId = model.StaffId;
			jobStaff.TractorId = model.TractorId;
			jobStaff.TrailerId = model.TrailerId;

			mJobStaffRepository.Add(jobStaff);
			await mJobStaffRepository.SaveChangeAsync();

			return Ok();
		}


		[HttpPost("{id}")]
		public IActionResult Update(int id, [FromForm] JobStaffModel model)
		{
			var jobStaff = mJobStaffRepository.Query()
				.FirstOrDefault(x => x.Id == id);

			if (jobStaff == null)
			{
				return NotFound();
			}
			
			jobStaff.JobId = model.JobId;
			jobStaff.StaffId = model.StaffId;
			jobStaff.TractorId = model.TractorId;
			jobStaff.TrailerId = model.TrailerId;

			mJobStaffRepository.Update(jobStaff);

			return Ok();
		}
		
		[HttpPost("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var jobStaff = mJobStaffRepository.Query()
				.FirstOrDefault(x => x.Id == id);

			if (jobStaff == null)
			{
				return NotFound();
			}

			mJobStaffRepository.Remove(jobStaff);
			await mJobStaffRepository.SaveChangeAsync();

			return Ok();
		}
	}
}
