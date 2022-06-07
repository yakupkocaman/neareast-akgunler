using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Akgunler.Models.Vehicles
{
    [Table("VehicleGroup", Schema = "Vehicle")]
    public class VehicleGroup : EntityBase
    {
        [Key]
        [Column("VehicleGroupId")]
        public override int Id { get; set; }

        public string Name { get; set; }
    }
}
