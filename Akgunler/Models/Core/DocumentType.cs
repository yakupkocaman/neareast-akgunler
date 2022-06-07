using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Akgunler.Models.Core
{
    public enum DocumentGroup
    {
        Customer = 1,
        Staff = 2,
        Vehicle = 3
    }

    [Table("DocumentType", Schema = "Core")]
    public class DocumentType : EntityBase
    {
        [Key]
        [Column("DocumentTypeId")]
        public override int Id { get; set; }


        public string Name { get; set; }
        public DocumentGroup DocumentGroup { get; set; }

    }
}