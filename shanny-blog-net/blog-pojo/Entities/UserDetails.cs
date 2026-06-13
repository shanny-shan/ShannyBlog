using blog_common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace blog_pojo.Entities
{
    [Table("user_details")]
    public class UserDetails
    {
        [Key]
        [MaxLength(50)]
        [Required]
        [Column("uuid")]
        public string Uuid { get; set; } = string.Empty;

        [MaxLength(30)]
        [Column("nickname")]
        public string Nickname { get; set; } = string.Empty;

        [MaxLength(30)]
        [Column("username")]
        public string Username { get; set; } = string.Empty;

        [Column("birthday")]
        public DateOnly? Birthday { get; set; }

        [Required]
        [Column("sex")]
        public UserSex Sex { get; set; }

        [MaxLength(500)]
        [Column("avatar")]
        public string Avatar { get; set; } = string.Empty;

        [Required]
        [Column("createTime")]
        public DateTime CreateTime { get; set; }

        [Required]
        [Column("updateTime")]
        public DateTime UpdateTime { get; set; }
    }
}
