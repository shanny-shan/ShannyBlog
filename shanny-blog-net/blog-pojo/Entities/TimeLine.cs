using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace blog_pojo.Entities
{
    [Table("timelines")]
    public class TimeLine
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [Column("articleId")]
        public long ArticleId { get; set; }

        [Required]
        [Column("time")]
        public DateTime Time { get; set; }

        [MaxLength(50)]
        [Required]
        [Column("title")]
        public string Title { get; set; } = string.Empty;

        [MaxLength(500)]
        [Required]
        [Column("text")]
        public string Text { get; set; } = string.Empty;
    }
}
