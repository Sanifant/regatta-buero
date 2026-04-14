using LRV.Regatta.Buero.Interfaces;
using LRV.Regatta.Buero.Models;
using LRV.Regatta.Buero.Services;
using LRV.Regatta.Buero.Tests.Helpers;
using Microsoft.Extensions.Logging.Abstractions;

namespace LRV.Regatta.Buero.Tests.Mocks
{
    internal sealed class MockLogService : ILogService, IDisposable
    {
        private readonly DatabaseContext databaseContext;
        private readonly LogService logService;

        public MockLogService()
        {
            this.databaseContext = DatabaseContextFactory.Create($"MockLogService_{Guid.NewGuid()}");
            this.logService = new LogService(this.databaseContext, NullLogger<LogService>.Instance);
        }

        public List<LogObject> Logs => this.logService.GetLogs();

        public void Seed(params LogObject[] logs)
        {
            this.databaseContext.LogObjects.AddRange(logs);
            this.databaseContext.SaveChanges();
        }

        public void AddLog(LogObject log)
        {
            this.logService.AddLog(log);
        }

        public List<LogObject> GetLogs()
        {
            return this.logService.GetLogs();
        }

        public PagedResult<LogObject> GetPaginatedLogs(int page, int pageSize)
        {
            return this.logService.GetPaginatedLogs(page, pageSize);
        }

        public void Dispose()
        {
            this.databaseContext.Dispose();
        }
    }
}