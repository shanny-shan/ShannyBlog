using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace blog_pojo.Entities
{
    [Table("abouts")]
    public class About
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [Column("isActive")]
        public bool IsActive { get; set; }

        [MaxLength(30)]
        [Required]
        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [MaxLength(300)]
        [Required]
        [Column("introduce")]
        public string Introduce { get; set; } = string.Empty;

        [MaxLength(500)]
        [Required]
        [Column("avatar")]
        public string Avatar { get; set; } = string.Empty;

        [MaxLength(200)]
        [Required]
        [Column("tag")]
        public string Tag { get; set; } = string.Empty;

        [MaxLength(200)]
        [Column("github")]
        public string Github { get; set; } = string.Empty;

        [MaxLength(200)]
        [Column("steam")]
        public string Steam { get; set; } = string.Empty;

        [MaxLength(200)]
        [Column("web")]
        public string Web { get; set; } = string.Empty;

        [MaxLength(200)]
        [Column("biliBili")]
        public string BiliBili { get; set; } = string.Empty;

        [MaxLength(200)]
        [Column("other")]
        public string Other { get; set; } = string.Empty;

        [Required]
        [Column("createTime")]
        public DateTime CreateTime { get; set; }

        [Required]
        [Column("updateTime")]
        public DateTime UpdateTime { get; set; }
    }
}
