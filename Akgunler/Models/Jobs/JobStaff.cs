using Akgunler.Models.Staffs;
using Akgunler.Models.Vehicles;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Akgunler.Models.Jobs
{
    [Table("JobStaff", Schema = "Job")]
    public class JobStaff : EntityBase
    {
        [Key]
        [Column("JobStaffId")]
        public override int Id { get; set; }


        public int JobId { get; set; }
        public int StaffId { get; set; }
        public int TractorId { get; set; }
        public int? TrailerId { get; set; }

        public virtual Job Job { get; set; }
        public virtual Staff Staff { get; set; }
        public virtual Vehicle Tractor { get; set; }
        public virtual Vehicle Trailer { get; set; }
    }
}
