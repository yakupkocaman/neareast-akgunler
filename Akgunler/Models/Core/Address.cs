using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Akgunler.Models.Core
{
    [Table("Address", Schema = "Core")]
    public class Address : EntityBase
    {
        [Key]
        [Column("AddressId")]
        public override int Id { get; set; }

        public string Name { get; set; }

        public int AddressTypeId { get; set; }
        public virtual AddressType AddressType { get; set; }

        public string AddressLine { get; set; }

        public int? DistrictId { get; set; }
        public virtual District District { get; set; }

        public string PostalCode { get; set; }
        public string Note { get; set; }
        public bool IsActive { get; set; }

        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}