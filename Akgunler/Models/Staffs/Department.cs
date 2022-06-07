using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Akgunler.Models.Staffs
{
    [Table("Department", Schema = "Staff")]
    public class Department : EntityBase
    {
        [Key]
        [Column("DepartmentId")]
        public override int Id { get; set; }

        public string Name { get; set; }
    }
}