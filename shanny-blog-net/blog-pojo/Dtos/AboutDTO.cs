namespace blog_pojo.Dtos
{
    public class AboutDTO
    {
        public long Id { get; set; }
        public string Avatar { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Introduce { get; set; } = string.Empty;
        public string Tag { get; set; } = string.Empty;
        public string Github { get; set; } = string.Empty;
        public string Steam { get; set; } = string.Empty;
        public string Web { get; set; } = string.Empty;
        public string BiliBili { get; set; } = string.Empty;
        public bool? IsActive { get; set; }
        public string Other { get; set; } = string.Empty;
    }
}
