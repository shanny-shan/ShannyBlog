using blog_common.Enums;
using blog_pojo.Entities;

namespace blog_pojo.Vos
{
    public class UserInfoVO
    {
        public string Uuid { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public UserStatus Status { get; set; }
        public UserType Type { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public UserDetails? UserDetails { get; set; }
    }
}
