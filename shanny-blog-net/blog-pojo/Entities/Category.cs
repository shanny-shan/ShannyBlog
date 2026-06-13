using blog_common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace blog_pojo.Entities
{
    [Table("categories")]
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long Id { get; set; }

        [MaxLength(10)]
        [Required]
        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [MaxLength(10)]
        [Required]
        [Column("nameEn")]
        public string NameEn { get; set; } = string.Empty;

        [Required]
        [Column("type")]
        public CategoryType Type { get; set; }

        [Required]
        [Column("sort")]
        public int Sort { get; set; } = 0;
    }
}
