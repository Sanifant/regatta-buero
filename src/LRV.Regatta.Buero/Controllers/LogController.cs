namespace LRV.Regatta.Buero.Controllers
{
    using LRV.Regatta.Buero.Attributes;
    using LRV.Regatta.Buero.Models;
    using LRV.Regatta.Buero.Services;
    using Microsoft.AspNetCore.Mvc;

    [ApiKey]
    [ApiController]
    [Route("api/[controller]")]
    public class LogController : ControllerBase
    {
        private readonly ILogService logService;

        public LogController(ILogService logService)
        {
            this.logService = logService;
        }

        [HttpPost]
        public IActionResult Post([FromBody] List<LogObject> logs)
        {
            var ip = Request.Headers["X-Forwarded-For"].FirstOrDefault()
                ?? HttpContext.Connection.RemoteIpAddress?.ToString();

            foreach (var log in logs)
            {
                log.ClientIp = String.IsNullOrEmpty(ip) ? ip : "unknown";
                log.CreatedDate = DateTime.UtcNow;
                log.ClientName = Request.Headers["X-Client-Name"].FirstOrDefault() ?? log.ClientName;
                log.ClientVersion = Request.Headers["X-Client-Version"].FirstOrDefault() ?? log.ClientVersion;
                log.Message = log.Message ?? "dummy";
                log.Exception = log.Exception ?? null;

                logService.AddLog(log);
            }

            return Ok(new { success = true, received = logs.Count });
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(logService.GetLogs());
        }
    }

}
