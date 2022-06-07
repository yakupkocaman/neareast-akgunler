using Akgunler.Models.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Akgunler.Models.Customers
{
    [Table("CustomerAddress", Schema = "Customer")]
	public class CustomerAddress : EntityBase
	{
		[Key]
		[Column("CustomerAddressId")]
		public override int Id { get; set; }

		public int CustomerId { get; set; }
		public int AddressId { get; set; }

		public virtual Customer Customer { get; set; }
		public virtual Address Address { get; set; }
	}
}
