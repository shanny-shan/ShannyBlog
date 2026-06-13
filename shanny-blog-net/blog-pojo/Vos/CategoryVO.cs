using blog_common.Enums;

namespace blog_pojo.Vos
{
    public class CategoryVO
    {
        public long? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string NameEn { get; set; } = string.Empty;
        public CategoryType Type { get; set; }
        public int? Sort { get; set; }
    }
}
