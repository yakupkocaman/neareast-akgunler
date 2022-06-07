using Akgunler.Models.Core;
using Akgunler.Models.Customers;
using Akgunler.Models.Staffs;
using Akgunler.Models.Vehicles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Akgunler.Models.Jobs
{
    public enum JobReportStatus
	{
		Waiting = 0,
		Started = 1,
		Finished = 2,
		Cancelled = 3
	}

	[Table("Job", Schema = "Job")]
	public class Job : EntityBase
	{
		[Key]
		[Column("JobId")]
		public override int Id { get; set; }

		[Range(1, Int32.MaxValue)]
		public int JobTypeId { get; set; }
		public virtual JobType JobType { get; set; }
		[Range(1, Int32.MaxValue)]
		public int SenderId { get; set; }
		public virtual Customer Sender{ get; set; }
		public int? ReceiverId { get; set; }
		public virtual Customer Receiver{ get; set; }
		public int TransferTypeId { get; set; }
		public int? DeparturePortId { get; set; }
		public virtual Port DeparturePort { get; set; }
		public int? ArrivalPortId { get; set; }
		public virtual Port ArrivalPort { get; set; }
		public int? DepartureShipId { get; set; }
		public virtual Ship DepartureShip { get; set; }
		public int? ArrivalShipId { get; set; }
		public virtual Ship ArrivalShip { get; set; }
		public int? CustomsId { get; set; }
		public Customs Customs { get; set; }
		[Column(TypeName = "decimal(18,4)")]
		public decimal ExchangeRateUsd { get; set; }
		[Column(TypeName = "decimal(18,4)")]
		public decimal ExchangeRateEur { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? FinishDate { get; set; }
		public string Note { get; set; }
		public string Status { get; set; }
		public bool IsRf { get; set; }
		public bool IsActive { get; set; }
		public bool IsCancelled { get; set; }
		public bool HasUpdate { get; set; }

		public int StaffId { get; set; }
		public int TractorId { get; set; }
		public int? TrailerId { get; set; }

		public virtual Staff Staff { get; set; }
		public virtual Vehicle Tractor { get; set; }
		public virtual Vehicle Trailer { get; set; }

		[Timestamp]
		public virtual byte[] RowVersion { get; set; }

		public int? CreatedBy { get; set; }
		public int? UpdatedBy { get; set; }
		public int? DeletedBy { get; set; }
		public DateTime? CreatedOn { get; set; }
		public DateTime? UpdatedOn { get; set; }
		public DateTime? DeletedOn { get; set; }

		public virtual ICollection<Freight> Freights { get; set; }
	}
}
