namespace Akgunler.ViewModels.Jobs
{
	public class JobCountModel
	{
		public int? WaitingCount { get; set; }
		public int? TodayCount { get; set; }
		public int? ActiveCount { get; set; }
		public int? CompletedCount { get; set; }
		public int? AllCount { get; set; }
	}
}
