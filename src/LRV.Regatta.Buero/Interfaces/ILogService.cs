using LRV.Regatta.Buero.Models;

namespace LRV.Regatta.Buero.Interfaces
{
    /// <summary>
    /// ILogService interface that defines the contract for managing LogObject instances. This interface includes methods for adding and retrieving log data, as well as a method for retrieving paginated log data, allowing for the implementation of various data storage solutions (e.g., in-memory, database) while maintaining a consistent API for handling log information in the context of a regatta management system.
    /// </summary>
    public interface ILogService
    {
        /// <summary>
        /// Adds a LogObject instance to the internal list of log objects. This method allows for adding new log data to the in-memory collection, enabling the management of log information without the need for a persistent database.
        /// </summary>
        /// <param name="log">The LogObject instance to be added to the in-memory collection.</param>
        void AddLog(LogObject log);

        /// <summary>
        /// Retrieves all LogObject instances from the internal list. This method returns the in-memory collection of log data, allowing for read operations without accessing a persistent database.
        /// </summary>
        /// <returns>A list of all LogObject instances in the in-memory collection.</returns>
        List<LogObject> GetLogs();

        /// <summary>
        /// Retrieves a paginated list of LogObject instances from the internal list. This method allows for fetching a subset of log data based on the specified page number and page size, enabling efficient handling of large log collections without accessing a persistent database.
        /// </summary>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of log entries per page.</param>
        /// <returns>A PagedResult containing the LogObject instances for the specified page.</returns>
        PagedResult<LogObject> GetPaginatedLogs(int page, int pageSize);
    }
}
