using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Akgunler.Models.Core
{
    [Table("Country", Schema = "Core")]
    public class Country : EntityBase<string>
    {
        [Key]
        [StringLength(2)]
        [Column("CountryId")]
        public override string Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string Nationality { get; set; }

    }
}
