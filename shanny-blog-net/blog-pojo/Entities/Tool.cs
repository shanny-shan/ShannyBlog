using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace blog_pojo.Entities
{
    [Table("tools")]
    public class Tool
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long Id { get; set; }

        [MaxLength(50)]
        [Required]
        [Column("title")]
        public string Title { get; set; } = string.Empty;

        [MaxLength(300)]
        [Column("content")]
        public string Content { get; set; } = string.Empty;

        [MaxLength(300)]
        [Column("image")]
        public string Image { get; set; } = string.Empty;

        [MaxLength(300)]
        [Required]
        [Column("href")]
        public string Href { get; set; } = string.Empty;

        [Column("tags")]
        public List<long> Tags { get; set; } = new();

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
