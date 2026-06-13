using Microsoft.AspNetCore.Mvc;

namespace blog_server.Annotatin
{
    public class WebMvcConfig
    {
        private readonly JwtTokenUserInterceptor _jwtTokenUserInterceptor;
        private readonly ILogger<WebMvcConfig> _log;

        public WebMvcConfig(JwtTokenUserInterceptor jwtTokenUserInterceptor, ILogger<WebMvcConfig> log)
        {
            _jwtTokenUserInterceptor = jwtTokenUserInterceptor;
            _log = log;
        }

        public void AddInterceptors(MvcOptions options)
        {
            _log.LogInformation("开始注册自定义拦截器...");

            options.Filters.AddService<JwtTokenUserInterceptor>();
        }

        public static readonly string[] ExcludePaths =
        {
            "/account/login",
            "/account/register",
            "/about/show",
            "/article/recent",
            "/article/views",
            "/article/type",
            "/article/tag",
            "/article/id",
            "/category/all",
            "/tag/all",
            "/project/info",
            "/tool/all"
        };
    }
}
