using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Akgunler.Models.Staffs
{
    [Table("DriversLicenseType", Schema = "Staff")]
    public class DriversLicenseType : EntityBase
    {
        [Key]
        [Column("DriversLicenseTypeId")]
        public override int Id { get; set; }

        public string Name { get; set; }
    }
}
