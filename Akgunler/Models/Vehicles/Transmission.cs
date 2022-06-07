using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Akgunler.Models.Vehicles
{
    [Table("Transmission", Schema = "Vehicle")]
    public class Transmission : EntityBase
    {
        [Key]
        [Column("TransmissionId")]
        public override int Id { get; set; }

        public string Name { get; set; }
    }
}