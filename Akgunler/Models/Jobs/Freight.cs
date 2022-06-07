using Akgunler.Models.Core;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Akgunler.Models.Jobs
{
    [Table("Freight", Schema = "Job")]
    public class Freight : EntityBase
    {
        [Key]
        [Column("FreightId")]
        public override int Id { get; set; }

        public int JobId { get; set; }
        public virtual Job Job { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }

        public string ShippingAddressLine { get; set; }

        public int? ShippingDistrictId { get; set; }
        public virtual District ShippingDistrict { get; set; }


        public string DeliveryAddressLine { get; set; }

        public int? DeliveryDistrictId { get; set; }
        public virtual District DeliveryDistrict { get; set; }

        public string Note { get; set; }

		public bool IsDomesticShipping { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal DomesticShippingPrice { get; set; }

        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}