using System.ComponentModel;

namespace blog_common.Enums
{
    public enum ArticleType
    {
        [Description("NOTE")]
        Note,

        [Description("PROJECT")]
        Project,

        [Description("BUG")]
        Bug
    }
}
