using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Akgunler.Models.Core
{
    [Table("Log", Schema = "Core")]
    public class Log : EntityBase
    {
        [Key]
        [Column("LogId")]
        public override int Id { get; set; }

        public string Category { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Username { get; set; }
        public string IpAddress { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
