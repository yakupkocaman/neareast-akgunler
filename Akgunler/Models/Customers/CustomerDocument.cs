using Akgunler.Models.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Akgunler.Models.Customers
{
    [Table("CustomerDocument", Schema = "Customer")]
    public class CustomerDocument : EntityBase
    {
        [Key]
        [Column("CustomerDocumentId")]
        public override int Id { get; set; }

        public int CustomerId { get; set; }
        public int DocumentId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Document Document { get; set; }
    }
}
