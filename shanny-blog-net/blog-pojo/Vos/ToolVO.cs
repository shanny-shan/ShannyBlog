using blog_pojo.Entities;

namespace blog_pojo.Vos
{
    public class ToolVO
    {
        public long? Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public string Href { get; set; } = string.Empty;
        public List<long> Tags { get; set; } = new();
        public List<Tag> TagList { get; set; } = new();
        public bool? Published { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
