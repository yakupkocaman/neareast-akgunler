using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Akgunler.Models.Vehicles
{
    [Table("Vehicle", Schema = "Vehicle")]
    public class Vehicle : EntityBase
    {
        [Key]
        [Column("VehicleId")]
        public override int Id { get; set; }

        public string Name { get; set; }
        [NotMapped]
        public virtual string FullName => Make == null ? Name : string.Format("{0}, {1}, {2}, {3}, {4}", VehicleGroup?.Name, RegistrationNo, ModelYear, Make?.Name, Color?.Name);
        public string RegistrationNo { get; set; }
        public int? MakeId { get; set; }
        public virtual Make Make { get; set; }
        public string Model { get; set; }
        public int? ModelYear { get; set; }
        public int? ColorId { get; set; }
        public virtual Color Color { get; set; }

        public string ChassisNumber { get; set; }
        public string EngineNumber { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal? Mileage { get; set; }
        public DateTime? MileageDate { get; set; }
        public bool IsActive { get; set; }

        public int? VehicleTypeId { get; set; }
        public virtual VehicleType VehicleType { get; set; }

        public int? VehicleGroupId { get; set; }
        public virtual VehicleGroup VehicleGroup { get; set; }

        public int? FuelId { get; set; }
        public virtual Fuel Fuel { get; set; }

        public int? TransmissionId { get; set; }
        public virtual Transmission Transmission { get; set; }

        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
