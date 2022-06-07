using Akgunler.Models.Core;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Akgunler.Models.Jobs
{
    [Table("Account", Schema = "Job")]
    public class Account : EntityBase
    {
        [Key]
        [Column("AccountId")]
        public override int Id { get; set; }

        public int JobId { get; set; }
        public virtual Job Job { get; set; }
        public int AccountTypeId { get; set; }
        public virtual AccountType AccountType { get; set; }
        public int CurrencyId { get; set; }
        public virtual Currency Currency { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Debit { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Credit { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; }

        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
