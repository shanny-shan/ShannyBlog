using AspectCore.DynamicProxy;
using blog_common.Constant;
using blog_common.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using static blog_server.Annotatin.AutoFill;

namespace blog_server.Annotatin
{
    public class AutoFillAs : AbstractInterceptor
    {
        [FromServices]
        public ILogger<CorsConfig> Logger { get; set; } = null!;

        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            var method = context.ImplementationMethod;
            var attr = method.GetCustomAttribute<AutoFillAttribute>();

            if (attr != null)
            {
                Logger.LogInformation("开始处理公共字段自动填充");
                var opType = attr.Value;
                var args = context.Parameters;

                if (args == null || args.Length == 0)
                {
                    await next(context);
                    return;
                }

                object entity = args[0];
                DateTime now = DateTime.Now;

                if (opType == OperationType.Insert)
                {
                    var setCreate = entity.GetType().GetMethod(AutoFillConstant.SetCreateTime, new[] { typeof(DateTime) });
                    setCreate?.Invoke(entity, new object[] { now });
                }

                var setUpdate = entity.GetType().GetMethod(AutoFillConstant.SetUpdateTime, new[] { typeof(DateTime) });
                setUpdate?.Invoke(entity, new object[] { now });
            }

            await next(context);
        }
    }
}
