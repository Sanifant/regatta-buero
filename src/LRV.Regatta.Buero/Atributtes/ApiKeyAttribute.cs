using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LRV.Regatta.Buero.Attributes
{
    [AttributeUsage(validOn: AttributeTargets.Class)]
    public class ApiKeyAttribute : Attribute, IAsyncActionFilter
    {
        public static string APIKEYNAME = "X-API-KEY";

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(APIKEYNAME, out var extractedApiKey))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "Not Authorized"
                };
                return;
            }
            var appSettings = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var apiKey = String.IsNullOrEmpty(Environment.GetEnvironmentVariable(APIKEYNAME)) ?
                Environment.GetEnvironmentVariable(APIKEYNAME) :
                appSettings.GetValue<string>(APIKEYNAME);
            if (!apiKey.Equals(extractedApiKey))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "Not Authorized"
                };
                return;
            }
            await next();
        }
    }
}
