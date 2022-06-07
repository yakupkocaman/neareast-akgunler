using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Akgunler.Models.Core
{

    [Table("Document", Schema = "Core")]
    public class Document : EntityBase
    {
        [Key]
        [Column("DocumentId")]
        public override int Id { get; set; }

        public string FileName { get; set; }
        public string FileType { get; set; }

        public int? DocumentTypeId { get; set; }
        public virtual DocumentType DocumentType { get; set; }

        public string Name { get; set; }
        public DateTime Date { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Total { get; set; }

        public int? CurrencyId { get; set; }
        public virtual Currency Currency { get; set; }

        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }

    }
}
