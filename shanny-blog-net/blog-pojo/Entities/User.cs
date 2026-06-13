using blog_common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace blog_pojo.Entities
{
    [Table("users")]
    public class User
    {
        [Key]
        [MaxLength(50)]
        [Column("uuid")]
        public string Uuid { get; set; } = string.Empty;

        [MaxLength(30)]
        [Required]
        [Column("userId")]
        public string UserId { get; set; } = string.Empty;

        [MaxLength(11)]
        [Required]
        [Column("mobile")]
        public string Mobile { get; set; } = string.Empty;

        [MaxLength(100)]
        [Required]
        [Column("password")]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Column("status")]
        public UserStatus Status { get; set; }

        [Required]
        [Column("type")]
        public UserType Type { get; set; }

        [Required]
        [Column("createTime")]
        public DateTime CreateTime { get; set; }

        [Required]
        [Column("updateTime")]
        public DateTime UpdateTime { get; set; }

        [Column("lastLoginTime")]
        public DateTime? LastLoginTime { get; set; }
    }
}
