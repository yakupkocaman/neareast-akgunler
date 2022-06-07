using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Akgunler.Models.Core
{
    [Table("Role", Schema = "Core")]
    public class Role : EntityBase
    {
        [Key]
        [Column("RoleId")]
        public override int Id { get; set; }

        public string Name { get; set; }
        public string Title { get; set; }
    }
}
