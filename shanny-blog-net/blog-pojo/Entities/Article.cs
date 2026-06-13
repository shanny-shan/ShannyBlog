using blog_common.Enums;
using blog_pojo.Converters;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace blog_pojo.Entities
{
    [Table("articles")]
    public class Article
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long Id { get; set; }

        [MaxLength(50)]
        [Required]
        [Column("title")]
        public string Title { get; set; } = string.Empty;

        [Column("content", TypeName = "TEXT")]
        [Required]
        public string Content { get; set; } = string.Empty;

        [Column("memo", TypeName = "TEXT")]
        [Required]
        public string Memo { get; set; } = string.Empty;

        [MaxLength(300)]
        [Column("image")]
        public string Image { get; set; } = string.Empty;

        [MaxLength(300)]
        [Column("href")]
        public string Href { get; set; } = string.Empty;

        [Column("tags")]
        public List<long> Tags { get; set; } = new();

        [Required]
        [Column("type")]
        public CategoryType Type { get; set; }

        [Column("categoryId")]
        public long? CategoryId { get; set; }

        [Required]
        [Column("timelines")]
        public List<long> Timelines { get; set; } = new();

        [Required]
        [Column("views")]
        public int Views { get; set; } = 0;

        [Required]
        [Column("published")]
        public bool Published { get; set; } = true;

        [Required]
        [Column("createTime")]
        public DateTime CreateTime { get; set; }

        [Required]
        [Column("updateTime")]
        public DateTime UpdateTime { get; set; }
    }
}
