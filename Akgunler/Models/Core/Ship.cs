using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Akgunler.Models.Core
{
    [Table("Ship", Schema = "Core")]
    public class Ship : EntityBase
    {
        [Key]
        [Column("ShipId")]
        public override int Id { get; set; }

        public string Name { get; set; }
        public string Code { get; set; }
    }
}
