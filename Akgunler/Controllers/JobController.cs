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
	public class JobController : Controller
    {
        private readonly IRepository<Job> mJobRepository;

        public JobController(
			IRepository<Job> jobRepository
		)
		{
			mJobRepository = jobRepository;
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromForm] JobStaffModel model)
		{
			var job = new Job();
			job.Id = model.JobId;
			job.StaffId = model.StaffId;
			job.TractorId = model.TractorId;
			job.TrailerId = model.TrailerId;

			mJobRepository.Add(job);
			await mJobRepository.SaveChangeAsync();

			return Ok();
		}


		[HttpPost("{id}")]
		public IActionResult Update(int id, [FromForm] JobStaffModel model)
		{
			var job = mJobRepository.Query()
				.FirstOrDefault(x => x.Id == id);

			if (job == null)
			{
				return NotFound();
			}
			
			job.Id = model.JobId;
			job.StaffId = model.StaffId;
			job.TractorId = model.TractorId;
			job.TrailerId = model.TrailerId;

			mJobRepository.Update(job);

			return Ok();
		}
		
		[HttpPost("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var job = mJobRepository.Query()
				.FirstOrDefault(x => x.Id == id);

			if (job == null)
			{
				return NotFound();
			}

			mJobRepository.Remove(job);
			await mJobRepository.SaveChangeAsync();

			return Ok();
		}
	}
}
