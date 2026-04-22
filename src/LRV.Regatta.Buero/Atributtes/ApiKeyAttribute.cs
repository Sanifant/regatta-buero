using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LRV.Regatta.Buero.Attributes
{
    /// <summary>
    /// Action-Filter-Attribut zur API-Key-Authentifizierung.
    /// </summary>
    /// <remarks>
    /// Das Attribut liest den API-Key aus dem Request-Header <c>X-API-KEY</c> und vergleicht ihn mit
    /// dem konfigurierten Wert. Der Schlüssel wird in folgender Reihenfolge gesucht:
    /// <list type="number">
    ///   <item>Umgebungsvariable <c>API_KEY</c></item>
    ///   <item>Umgebungsvariable <c>X_API_KEY</c> (Legacy)</item>
    ///   <item>Konfigurationseintrag <c>API_KEY</c></item>
    ///   <item>Konfigurationseintrag <c>X-API-KEY</c></item>
    /// </list>
    /// Das Attribut kann nur auf Klassen angewendet werden.
    /// </remarks>
    [AttributeUsage(validOn: AttributeTargets.Class)]
    public class ApiKeyAttribute : Attribute, IAsyncActionFilter
    {
        private const string APIKEYNAME = "X-API-KEY";
        private const string APIKEYCONFIGURATIONNAME = "API_KEY";
        private const string APIKEYLEGACYCONFIGURATIONNAME = "X_API_KEY";

        /// <summary>
        /// Führt die API-Key-Validierung vor der Action-Ausführung durch.
        /// </summary>
        /// <param name="context">Der Kontext der aktuellen Action-Ausführung.</param>
        /// <param name="next">Der Delegat zum Aufrufen der nächsten Action im Filter-Pipeline.</param>
        /// <returns>Ein <see cref="Task"/>, der die asynchrone Operation darstellt.</returns>
        /// <remarks>
        /// Ist kein <c>X-API-KEY</c>-Header vorhanden oder stimmt der Wert nicht überein,
        /// wird die Anfrage abgebrochen. Ist serverseitig kein API-Key konfiguriert,
        /// wird ein interner Serverfehler zurückgegeben.
        /// </remarks>
        /// <response code="401">Kein oder ungültiger API-Key im Request-Header.</response>
        /// <response code="500">Serverseitig ist kein API-Key konfiguriert.</response>
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
            if (string.IsNullOrEmpty(apiKey))
            {
                apiKey = Environment.GetEnvironmentVariable(APIKEYLEGACYCONFIGURATIONNAME);
            }
            if (string.IsNullOrEmpty(apiKey))
            {
                apiKey = appSettings.GetValue<string>(APIKEYCONFIGURATIONNAME);
            }
            if (string.IsNullOrEmpty(apiKey))
            {
                apiKey = appSettings.GetValue<string>(APIKEYNAME);
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
