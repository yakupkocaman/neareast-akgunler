using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Akgunler.Models.Vehicles
{
    [Table("Color", Schema = "Vehicle")]
    public class Color : EntityBase
    {
        [Key]
        [Column("ColorId")]
        public override int Id { get; set; }

        public string Name { get; set; }
    }
}
