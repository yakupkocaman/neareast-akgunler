using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Akgunler.Models.Customers
{
    [Table("CustomerContact", Schema = "Customer")]
	public class CustomerContact : EntityBase
	{
		[Key]
		[Column("CustomerContactId")]
		public override int Id { get; set; }

		public int CustomerId { get; set; }
		public int ContactId { get; set; }

		public virtual Customer Customer { get; set; }
		public virtual Contact Contact { get; set; }
	}
}
