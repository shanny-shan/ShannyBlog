using blog_common.Enums;

namespace blog_pojo.Dtos
{
    public class ArticleDTO
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Memo { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public string Href { get; set; } = string.Empty;
        public List<long> Tags { get; set; } = new();
        public CategoryType Type { get; set; }
        public long CategoryId { get; set; }
        public List<long> Timelines { get; set; } = new();
        public int Views { get; set; } = 0;
        public bool Published { get; set; } = true;
    }
}
