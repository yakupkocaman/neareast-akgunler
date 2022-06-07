using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Akgunler.Models.Core
{
    [Table("Province", Schema = "Core")]
    public class Province : EntityBase
    {
        [Key]
        [Column("ProvinceId")]
        public override int Id { get; set; }

        public string Name { get; set; }

        [StringLength(2)]
        public string CountryId { get; set; }
        public virtual Country Country { get; set; }
    }
}
