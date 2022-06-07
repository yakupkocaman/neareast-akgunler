using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Akgunler.Models.Jobs
{
    [Table("JobType", Schema = "Job")]
    public class JobType : EntityBase
    {
        [Key]
        [Column("JobTypeId")]
        public override int Id { get; set; }

        public string Name { get; set; }
    }
}