using LRV.Regatta.Buero.Models;
using LRV.Regatta.Buero.Services;
using LRV.Regatta.Buero.Tests.Helpers;
using Microsoft.Extensions.Logging.Abstractions;

namespace LRV.Regatta.Buero.Tests.Service
{
    [TestClass]
    public class LogServiceTests
    {
        private DatabaseContext CreateContext() =>
            DatabaseContextFactory.Create($"LogServiceTests_{Guid.NewGuid()}");

        [TestMethod]
        public void AddLog_PersistsItem()
        {
            using var ctx = CreateContext();
            var svc = new LogService(ctx, NullLogger<LogService>.Instance);

            svc.AddLog(new LogObject { Id = 1, ClientName = "Client", ClientVersion = "1.0", Message = "Test" });

            Assert.AreEqual(1, ctx.LogObjects.Count());
        }

        [TestMethod]
        public void GetLogs_ReturnsAllItems()
        {
            using var ctx = CreateContext();
            ctx.LogObjects.Add(new LogObject { Id = 1, ClientName = "Client A", ClientVersion = "1.0", Message = "A" });
            ctx.LogObjects.Add(new LogObject { Id = 2, ClientName = "Client B", ClientVersion = "1.1", Message = "B" });
            ctx.SaveChanges();

            var svc = new LogService(ctx, NullLogger<LogService>.Instance);

            var result = svc.GetLogs();

            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void GetPaginatedLogs_ReturnsRequestedPage()
        {
            using var ctx = CreateContext();
            ctx.LogObjects.Add(new LogObject { Id = 1, ClientName = "Client A", ClientVersion = "1.0", Message = "A" });
            ctx.LogObjects.Add(new LogObject { Id = 2, ClientName = "Client B", ClientVersion = "1.1", Message = "B" });
            ctx.LogObjects.Add(new LogObject { Id = 3, ClientName = "Client C", ClientVersion = "1.2", Message = "C" });
            ctx.SaveChanges();

            var svc = new LogService(ctx, NullLogger<LogService>.Instance);

            var result = svc.GetPaginatedLogs(2, 2);

            Assert.AreEqual(3, result.TotalCount);
            Assert.AreEqual(1, result.Items.Count);
            Assert.AreEqual(3, result.Items[0].Id);
        }
    }
}