using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Akgunler.Models.Core
{
    public enum AddressGroup
    {
        Customer = 1,
        Staff = 2,
        Job = 3
    }

    [Table("AddressType", Schema = "Core")]
    public class AddressType : EntityBase
    {
        [Key]
        [Column("AddressTypeId")]
        public override int Id { get; set; }

        public string Name { get; set; }
        public AddressGroup AddressGroup { get; set; }

    }
}