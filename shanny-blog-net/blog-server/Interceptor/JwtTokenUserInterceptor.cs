using blog_common.Config;
using blog_common.Constant;
using blog_common.Context;
using blog_common.Utils;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace blog_server.Annotatin
{
    public class JwtTokenUserInterceptor : IAsyncActionFilter
    {
        private readonly JwtConfig _jwtConfig;
        private readonly ILogger<JwtTokenUserInterceptor> _log;

        public JwtTokenUserInterceptor(JwtConfig jwtConfig, ILogger<JwtTokenUserInterceptor> log)
        {
            _jwtConfig = jwtConfig;
            _log = log;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string path = context.HttpContext.Request.Path.Value;
            if (WebMvcConfig.ExcludePaths.Contains(path, StringComparer.OrdinalIgnoreCase))
            {
                await next();
                return;
            }

            var controllerAction = context.ActionDescriptor as Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor;
            if (controllerAction == null)
            {
                await next();
                return;
            }

            var httpContext = context.HttpContext;
            string token = httpContext.Request.Headers[_jwtConfig.UserTokenName];

            try
            {
                _log.LogInformation("jwt校验:{Token}", token);
                Dictionary<string, object> data = JwtUtil.ParseJwt(_jwtConfig.UserSecretKey, token);
                string userId = data[JwtClaimsConstant.UserId].ToString();
                BaseContext.SetCurrentId(userId);

                await next();
            }
            catch
            {
                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }
        }
    }
}
