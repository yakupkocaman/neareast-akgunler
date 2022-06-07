using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Akgunler.Models.Vehicles
{
    [Table("Make", Schema = "Vehicle")]
    public class Make : EntityBase
    {
        [Key]
        [Column("MakeId")]
        public override int Id { get; set; }

        public string Name { get; set; }
    }
}
