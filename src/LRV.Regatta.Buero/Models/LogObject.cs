namespace LRV.Regatta.Buero.Models
{
    public class LogObject
    {
        public int Id { get; set; }

        public string ClientIp { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Message { get; set; }

        public string? Exception { get; set; }
    }
}
