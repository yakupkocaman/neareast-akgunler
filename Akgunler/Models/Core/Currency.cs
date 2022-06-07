using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Akgunler.Models.Core
{
    [Table("Currency", Schema = "Core")]
    public class Currency : EntityBase
    {
        [Key]
        [Column("CurrencyId")]
        public override int Id { get; set; }

        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Sign { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

    }
}
