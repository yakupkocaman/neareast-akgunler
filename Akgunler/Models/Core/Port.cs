using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Akgunler.Models.Core
{
    [Table("Port", Schema = "Core")]
    public class Port : EntityBase
    {
        [Key]
        [Column("PortId")]
        public override int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public int DistrictId { get; set; }
        public District District { get; set; }
    }
}
