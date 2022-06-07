using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Akgunler.Models.Customers
{
    [Table("Customer", Schema = "Customer")]
    public class Customer : EntityBase
    {
        [Key]
        [Column("CustomerId")]
        public override int Id { get; set; }

        public int ExternalId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public int Classification { get; set; }
        public bool IsActive { get; set; }
        public int GroupId { get; set; }

        public virtual Group Group { get; set; }
        public virtual ICollection<CustomerContact> Contacts { get; set; }
        public virtual ICollection<CustomerAddress> Addresses { get; set; }
        public virtual ICollection<CustomerDocument> Documents { get; set; }

        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
