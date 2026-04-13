using LRV.Regatta.Buero.Interfaces;
using LRV.Regatta.Buero.Models;

namespace LRV.Regatta.Buero.Services
{
    /// <summary>
    /// Service class for managing log operations in the regatta management system.
    /// </summary>
    public class LogService : ILogService
    {
        private readonly DatabaseContext databaseContext;
        private readonly ILogger<LogService> logger;

        /// <summary>
        /// Constructor for the LogService class, initializing the DatabaseContext dependency.
        /// </summary>
        /// <param name="context">The DatabaseContext instance used for accessing the database.</param>
        /// <param name="logger">The ILogger instance used for logging.</param>
        public LogService(DatabaseContext context, ILogger<LogService> logger)
        {
            this.databaseContext = context;
            this.logger = logger;
        }

        /// <summary>
        /// Adds a new log entry to the database.
        /// </summary>
        /// <param name="log">The log entry to add.</param>
        public void AddLog(LogObject log)
        {
            this.databaseContext.LogObjects.Add(log);
            this.databaseContext.SaveChanges();
        }

        /// <summary>
        /// Retrieves all log entries from the database.
        /// </summary>
        /// <returns>A list of all log entries.</returns>
        public List<LogObject> GetLogs()
        {
            return this.databaseContext.LogObjects.ToList();
        }

        /// <summary>
        /// Retrieves a paginated list of log entries from the database based on the specified page number and page size.
        /// </summary>
        /// <remarks>
        /// The implementation of this method should query the database for log entries, applying pagination logic to 
        /// return only the log entries for the specified page. The method should calculate the total count of log 
        /// entries in the database to allow for proper pagination on the client side. The returned PagedResult object 
        /// should contain the list of log entries for the current page as well as the total count of log entries in 
        /// the database.
        /// </remarks>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of log entries per page.</param>
        /// <returns>A paginated result containing the log entries for the specified page.</returns>
        public PagedResult<LogObject> GetPaginatedLogs(int page, int pageSize)
        {
            var query = this.databaseContext.LogObjects.AsQueryable();

            var totalCount = query.Count();
            var items = query
                .OrderBy(i => i.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PagedResult<LogObject>()
            {
                Items = items,
                TotalCount = totalCount
            };

        }
    }
}