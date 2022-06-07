using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Akgunler.Models.Vehicles
{
    [Table("Fuel", Schema = "Vehicle")]
    public class Fuel : EntityBase
    {
        [Key]
        [Column("FuelId")]
        public override int Id { get; set; }

        public string Name { get; set; }
    }
}
