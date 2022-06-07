using Akgunler.Models.Staffs;
using Akgunler.Models.Vehicles;

namespace Akgunler.ViewModels.Jobs
{
	public class JobStaffModel
	{
		public int JobStaffId { get; set; }
		public int JobId { get; set; }
		public int StaffId { get; set; }
		public int TractorId { get; set; }
		public int? TrailerId { get; set; }

		public Staff Staff { get; set; }
		public Vehicle Vehicle { get; set; }
	}
}
