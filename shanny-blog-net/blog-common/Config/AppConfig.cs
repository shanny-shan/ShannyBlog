using System.Reflection;

namespace blog_common.Config
{
    public class AppConfig
    {
        public string Name { get; set; } = "ShannyBlog";
        public string Description { get; set; } = "个人博客系统";
        public string Owner { get; set; } = "Shanny";
        public string Version { get; set; } = string.Empty;
        public string BuildTime { get; set; } = string.Empty;
    }
}
