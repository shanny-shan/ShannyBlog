using blog_common.Enums;
using blog_pojo.Entities;

namespace blog_pojo.Dtos
{
    public class UserInfoDTO
    {
        public string Uuid { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public UserStatus Status { get; set; }
        public UserType Type { get; set; }
        public UserDetails UserDetails { get; set; } = new();
    }
}
