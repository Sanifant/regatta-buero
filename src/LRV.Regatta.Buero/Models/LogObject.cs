namespace LRV.Regatta.Buero.Models
{
    /// <summary>
    /// LogObject class representing the data structure for log information in the regatta management system. This class includes properties for Id, ClientIp, ClientName, ClientVersion, CreatedDate, Message, and Exception, allowing for the storage and retrieval of log data in the application. The LogObject can be used to manage and analyze log events effectively in the context of a regatta management system.
    /// </summary>
    public class LogObject
    {
        /// <summary>
        /// Gets or sets the unique identifier for the log object.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the client IP address for the log object. This property allows for the storage of the IP address associated with the log event, enabling the application to track and analyze log events based on client IP information in the regatta management system.
        /// </summary>
        public string ClientIp { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the client name for the log object. This property allows for the storage of the name of the client associated with the log event, enabling the application to track and analyze log events based on client information in the regatta management system.
        /// </summary>
        public string ClientName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the client version for the log object. This property allows for the storage of the version of the client associated with the log event, enabling the application to track and analyze log events based on client version information in the regatta management system.
        /// </summary>
        public string ClientVersion { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date and time when the log object was created. This property allows for the storage of the creation timestamp of the log event, enabling the application to track and analyze log events based on their occurrence time in the regatta management system.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the message for the log object. This property allows for the storage of the log message associated with the log event, enabling the application to track and analyze log events based on their messages in the regatta management system.
        /// </summary>
        public string Message { get; set; } = "dummy";

        /// <summary>
        /// Gets or sets the exception information for the log object. This property allows for the storage of any exception details associated with the log event, enabling the application to track and analyze log events based on their exceptions in the regatta management system.
        /// </summary>
        public string? Exception { get; set; }
    }
}
