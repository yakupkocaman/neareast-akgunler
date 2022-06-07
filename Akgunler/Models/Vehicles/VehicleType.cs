using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Akgunler.Models.Vehicles
{
    [Table("VehicleType", Schema = "Vehicle")]
    public class VehicleType : EntityBase
    {
        [Key]
        [Column("VehicleTypeId")]
        public override int Id { get; set; }

        public string Name { get; set; }

        public int VehicleGroupId { get; set; }
        public virtual VehicleGroup VehicleGroup { get; set; }
    }
}
