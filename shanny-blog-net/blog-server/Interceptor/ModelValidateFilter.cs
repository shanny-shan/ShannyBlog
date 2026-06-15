using blog_common.Result;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace blog_server.Interceptor
{
    public class ModelValidateFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                string errMsg = string.Join("，", context.ModelState
                    .SelectMany(x => x.Value.Errors)
                    .Select(e => e.ErrorMessage));

                var result = Result<object>.Error(errMsg);
                context.Result = new ObjectResult(result)
                {
                    StatusCode = 200
                };
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}