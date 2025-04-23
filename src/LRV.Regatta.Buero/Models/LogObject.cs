namespace LRV.Regatta.Buero.Models
{
    public class LogObject
    {
        public int Id { get; set; }

        public string ClientIp { get; set; } = string.Empty;

        public string ClientName { get; set; } = string.Empty;

        public string ClientVersion { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; }

        public string Message { get; set; } = "dummy";

        public string? Exception { get; set; }
    }
}
