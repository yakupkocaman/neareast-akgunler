using Akgunler.Models.Core;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Akgunler.Models.Staffs
{
    [Table("Staff", Schema = "Staff")]
    public class Staff : EntityBase
    {
        [Key]
        [Column("StaffId")]
        public override int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        [NotMapped]
        public virtual string FullName => string.Format("{0} {1}", FirstName, LastName);
        public string Title { get; set; }
        public bool IsActive { get; set; }

        [Range(1, Int32.MaxValue)]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        [StringLength(2)]
        public string NationalityId { get; set; }
        public virtual Country Nationality { get; set; }

        public string IdentityNo { get; set; }
        public string DocumentNo { get; set; }

        public string ForeignLangs { get; set; }

        [NotMapped]
        public virtual string[] ForeignLanguages { get; set; }

        public string Address { get; set; }
        public DateTime Birthday { get; set; }
        public string DriversLicenseNo { get; set; }
        public DateTime DriversLicenseExpiry { get; set; }

        [Range(1, Int32.MaxValue)]
        public int DriversLicenseTypeId { get; set; }
        public virtual DriversLicenseType DriversLicenseType { get; set; }

        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Phone3 { get; set; }
        public string Phone4 { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }

        public int? UserRoleId { get; set; }
        public virtual UserRole UserRole { get; set; }

        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}