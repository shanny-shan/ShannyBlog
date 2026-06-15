using blog_pojo.Entity;

namespace blog_pojo
{
    public class BaseEntity: TimeEntity
    {
        public string CreateUserId { get; set; } = string.Empty;
        public string UpdateUserId { get; set; } = string.Empty;
    }
}