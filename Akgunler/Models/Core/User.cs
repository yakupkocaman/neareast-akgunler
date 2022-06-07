using Akgunler.Data;
using Akgunler.Models.Staffs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;

namespace Akgunler.Models.Core
{
	[Table("User", Schema = "Core")]
	public class User : EntityBase
	{
		[Key]
		[Column("UserId")]
		public override int Id { get; set; }

		public string Username { get; set; }
		public string Password { get; set; }
		[NotMapped]
		public virtual string PasswordAgain { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public int? StaffId { get; set; }
		public virtual Staff Staff { get; set; }
		public bool IsActive { get; set; }

		public bool LockoutEnabled { get; set; }
		public DateTime? LockoutEndDate { get; set; }
		public int AccessFailedCount { get; set; }

		[JsonIgnore]
		public virtual List<UserRole> UserRoles { get; set; }

	}
}
