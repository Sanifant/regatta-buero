using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LRV.Regatta.Buero.Attributes
{
    [AttributeUsage(validOn: AttributeTargets.Class)]
    public class ApiKeyAttribute : Attribute, IAsyncActionFilter
    {
        public static string APIKEYNAME = "X-API-KEY";
        public static string APIKEYCONFIGURATIONNAME = "API_KEY";
        public static string APIKEYLEGACYCONFIGURATIONNAME = "X_API_KEY";

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
            var apiKey = Environment.GetEnvironmentVariable(APIKEYCONFIGURATIONNAME);
            if (String.IsNullOrEmpty(apiKey))
            {
                apiKey = Environment.GetEnvironmentVariable(APIKEYLEGACYCONFIGURATIONNAME);
            }
            if (String.IsNullOrEmpty(apiKey))
            {
                apiKey = appSettings.GetValue<string>(APIKEYCONFIGURATIONNAME);
            }
            if (String.IsNullOrEmpty(apiKey))
            {
                apiKey = appSettings.GetValue<string>(APIKEYLEGACYCONFIGURATIONNAME);
            }
            if(string.IsNullOrEmpty(apiKey))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 500,
                    Content = "API Key is not configured."
                };
                return;
            }


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
