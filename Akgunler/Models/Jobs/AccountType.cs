using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Akgunler.Models.Jobs
{
    [Table("AccountType", Schema = "Job")]
    public class AccountType : EntityBase
    {
        [Key]
        [Column("AccountTypeId")]
        public override int Id { get; set; }

        public string Name { get; set; }
    }
}
