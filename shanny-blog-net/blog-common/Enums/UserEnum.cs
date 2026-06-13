using System.ComponentModel;

namespace blog_common.Enums
{
    public enum UserSex
    {
        [Description("UNKNOWN")]
        Unknown,
        [Description("MAN")]
        Man,
        [Description("FEMALE")]
        Female
    }

    public enum UserStatus
    {
        [Description("ACTIVE")]
        Active,
        [Description("LOCKED")]
        Locked,
        [Description("DELETED")]
        Deleted
    }

    public enum UserType
    {
        [Description("AUTHOR")]
        Author,
        [Description("USER")]
        User
    }
}
