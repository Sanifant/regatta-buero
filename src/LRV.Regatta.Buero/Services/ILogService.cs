using LRV.Regatta.Buero.Models;

namespace LRV.Regatta.Buero.Services
{
    public interface ILogService
    {
        void AddLog(LogObject log);
        List<LogObject> GetLogs();
    }
}
