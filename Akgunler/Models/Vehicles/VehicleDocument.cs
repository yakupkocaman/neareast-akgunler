using Akgunler.Models.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Akgunler.Models.Vehicles
{

    [Table("VehicleDocument", Schema = "Vehicle")]
	public class VehicleDocument : EntityBase
	{
		[Key]
		[Column("VehicleDocumentId")]
		public override int Id { get; set; }

		public int VehicleId { get; set; }
		public int DocumentId { get; set; }

		public virtual Vehicle Vehicle { get; set; }
		public virtual Document Document { get; set; }
	}
}
