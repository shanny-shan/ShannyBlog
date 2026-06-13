using System.ComponentModel;

namespace blog_common.Enums
{
    public enum MediaType
    {
        [Description("BOOK")]
        Book,

        [Description("PHOTO")]
        Photo,

        [Description("VIDEO")]
        Video,

        [Description("MUSIC")]
        Music
    }
}
