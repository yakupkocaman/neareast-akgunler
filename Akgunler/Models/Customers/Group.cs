using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Akgunler.Models
{
    [Table("Group", Schema = "Customer")]
    public class Group : EntityBase
    {
        [Key]
        [Column("GroupId")]
        public override int Id { get; set; }

        public string Name { get; set; }
    }
}