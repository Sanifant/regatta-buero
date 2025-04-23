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

            var clientIp = ip ?? "unknown";

            foreach (var log in logs)
            {
                log.ClientIp = clientIp;
                log.CreatedDate = DateTime.UtcNow;
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
