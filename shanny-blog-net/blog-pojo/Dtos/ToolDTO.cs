namespace blog_pojo.Dtos
{
    public class ToolDTO
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public string Href { get; set; } = string.Empty;
        public List<long> Tags { get; set; } = new List<long>();
    }
}
