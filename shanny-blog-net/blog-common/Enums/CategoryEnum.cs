using System.ComponentModel;

namespace blog_common.Enums
{
    public enum CategoryType
    {
        [Description("ARTICLE_NOTE")]
        ArticleNote,

        [Description("ARTICLE_PROJECT")]
        ArticleProject,

        [Description("ARTICLE_BUG")]
        ArticleBug,

        [Description("MEDIA_BOOK")]
        MediaBook,

        [Description("MEDIA_PHOTO")]
        MediaPhoto,

        [Description("MEDIA_VIDEO")]
        MediaVideo,

        [Description("MEDIA_MUSIC")]
        MediaMusic
    }
}
