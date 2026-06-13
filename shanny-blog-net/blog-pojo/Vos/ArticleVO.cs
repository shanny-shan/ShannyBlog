using blog_common.Enums;
using blog_pojo.Entities;

namespace blog_pojo.Vos
{
    public class ArticleVO
    {
        public long? Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Memo { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public string Href { get; set; } = string.Empty;
        public List<long> Tags { get; set; } = new();
        public List<Tag> TagList { get; set; } = new();
        public CategoryType Type { get; set; }
        public long? CategoryId { get; set; }
        public CategoryVO? Category { get; set; }
        public List<long> Timelines { get; set; } = new();
        public int? Views { get; set; }
        public bool? Published { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
