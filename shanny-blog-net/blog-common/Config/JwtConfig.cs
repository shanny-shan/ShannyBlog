namespace blog_common.Config
{
    public class JwtConfig
    {
        public string UserSecretKey { get; set; } = string.Empty;
        public long UserTtl { get; set; }
        public string UserTokenName { get; set; } = string.Empty;
    }
}
