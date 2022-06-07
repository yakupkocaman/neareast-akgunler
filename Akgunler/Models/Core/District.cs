using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Akgunler.Models.Core
{
    [Table("District", Schema = "Core")]
    public class District : EntityBase
    {
        [Key]
        [Column("DistrictId")]
        public override int Id { get; set; }

        public string Name { get; set; }

        public int ProvinceId { get; set; }
        public virtual Province Province { get; set; }
    }
}
