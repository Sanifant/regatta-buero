namespace LRV.Regatta.Buero.Controllers
{
    using LRV.Regatta.Buero.Attributes;
    using LRV.Regatta.Buero.Interfaces;
    using LRV.Regatta.Buero.Models;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// LogController class that handles HTTP requests related to log data in the regatta management system. This controller includes endpoints for adding new log entries, retrieving all log entries, and retrieving paginated log entries. The controller uses the ILogService to manage log data and includes an API key attribute for securing the endpoints.
    /// </summary>
    [ApiKey]
    [ApiController]
    [Route("api/[controller]")]
    public class LogController : ControllerBase
    {
        private readonly ILogService logService;

        /// <summary>
        /// Constructor for the LogController class, initializing the ILogService dependency. This constructor sets up the necessary service for managing log data in the regatta management system, allowing the controller to handle log-related HTTP requests effectively.
        /// </summary>
        /// <param name="logService">The ILogService instance used for managing log data.</param>
        public LogController(ILogService logService)
        {
            this.logService = logService;
        }

        /// <summary>
        /// Processes the POST HTTP Verb, adding new log entries to the log service. This method accepts a list of LogObject instances from the request body, enriches each log entry with additional information such as client IP, client name, client version, and timestamps, and then adds the log entries to the log service for storage and retrieval.
        /// </summary>
        /// <param name="logs">A list of LogObject instances to be added to the log service.</param>
        /// <returns>An IActionResult indicating the result of the operation.</returns>
        [HttpPost]
        public IActionResult Post([FromBody] List<LogObject> logs)
        {
            var ip = Request.Headers["X-Forwarded-For"].FirstOrDefault()
                ?? HttpContext.Connection.RemoteIpAddress?.ToString();

            foreach (var log in logs)
            {
                log.ClientIp = string.IsNullOrEmpty(ip) ? ip : "unknown";
                log.CreatedDate = DateTime.UtcNow;
                log.ClientName = Request.Headers["X-Client-Name"].FirstOrDefault() ?? log.ClientName;
                log.ClientVersion = Request.Headers["X-Client-Version"].FirstOrDefault() ?? log.ClientVersion;
                log.Message = log.Message ?? "dummy";
                log.Exception = log.Exception ?? null;

                logService.AddLog(log);
            }

            return Ok(new { success = true, received = logs.Count });
        }

        /// <summary>
        /// Processes the GET HTTP Verb, retrieving all log entries from the log service. This method returns a list of all LogObject instances stored in the log service, allowing clients to access the complete log data for analysis and monitoring purposes.
        /// </summary>
        /// <returns>An IActionResult containing the list of all LogObject instances.</returns>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(logService.GetLogs());
        }

        /// <summary>
        /// Processes the GET HTTP Verb for paginated log retrieval, fetching a subset of log entries based on the specified page number and page size. This method allows clients to efficiently access log data in manageable chunks, improving performance and usability when dealing with large log collections.
        /// </summary>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of log entries per page.</param>
        /// <returns>An IActionResult containing the paginated list of LogObject instances.</returns>
        [HttpGet("search")]
        public ActionResult<PagedResult<LogObject>> GetItems([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;

            var result = logService.GetPaginatedLogs(page, pageSize);

            return Ok(result);
        }
    }

}
