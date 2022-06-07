using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Akgunler.Models.Core
{
    [Table("Language", Schema = "Core")]
    public class Language : EntityBase
    {
        [Key]
        [Column("LanguageId")]
        public override int Id { get; set; }

        public string Name { get; set; }
        public string Code { get; set; }
    }
}
