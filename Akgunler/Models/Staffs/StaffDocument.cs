using Akgunler.Models.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Akgunler.Models.Staffs
{
    [Table("StaffDocument", Schema = "Staff")]
	public class StaffDocument : EntityBase
	{
		[Key]
		[Column("StaffDocumentId")]
		public override int Id { get; set; }

		public int StaffId { get; set; }
		public int DocumentId { get; set; }

		public virtual Staff Staff { get; set; }
		public virtual Document Document { get; set; }
	}
}
