using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Akgunler.Models.Core
{
    [Table("UserRole", Schema = "Core")]
	public class UserRole : EntityBase
	{
		[Key]
		[Column("UserRoleId")]
		public override int Id { get; set; }

		public int UserId { get; set; }
		public int RoleId { get; set; }

		public virtual User User { get; set; }
		public virtual Role Role { get; set; }
	}
}
