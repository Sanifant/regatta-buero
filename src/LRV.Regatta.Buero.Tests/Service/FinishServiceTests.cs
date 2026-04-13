using LRV.Regatta.Buero.Models;
using LRV.Regatta.Buero.Services;
using LRV.Regatta.Buero.Tests.Helpers;
using Microsoft.Extensions.Logging.Abstractions;

namespace LRV.Regatta.Buero.Tests.Service
{
    [TestClass]
    public class FinishServiceTests
    {
        // Eindeutiger DB-Name pro Test verhindert Datenlecks zwischen Methoden
        private DatabaseContext CreateContext() =>
            DatabaseContextFactory.Create($"FinishServiceTests_{Guid.NewGuid()}");

        [TestMethod]
        public void AddFinishObject_PersistsItem()
        {
            using var ctx = CreateContext();
            var svc = new FinishService(ctx, NullLogger<FinishService>.Instance);

            svc.AddFinishObject(new FinishObject { Id = 1, Name = "Zieleinlauf 1" });

            Assert.AreEqual(1, ctx.FinishObjects.Count());
        }

        [TestMethod]
        public void GetAllFinishObject_ReturnsDescendingOrder()
        {
            using var ctx = CreateContext();
            var svc = new FinishService(ctx, NullLogger<FinishService>.Instance);

            ctx.FinishObjects.Add(new FinishObject { Id = 1, Name = "Erster" });
            ctx.FinishObjects.Add(new FinishObject { Id = 2, Name = "Zweiter" });

            var result = svc.GetAllFinishObject();

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(2, result[0].Id);
        }

        [TestMethod]
        public void DeleteAllFinishObject_ClearsTable()
        {
            using var ctx = CreateContext();
            var svc = new FinishService(ctx, NullLogger<FinishService>.Instance);

            ctx.FinishObjects.Add(new FinishObject { Id = 1, Name = "Erster" });
            ctx.FinishObjects.Add(new FinishObject { Id = 2, Name = "Zweiter" });
            
            svc.DeleteAllFinishObject();

            Assert.AreEqual(0, ctx.FinishObjects.Count());
        }

        [TestMethod]
        public void DeleteFinishObject_RemovesSpecificItem()
        {
            using var ctx = CreateContext();
            var svc = new FinishService(ctx, NullLogger<FinishService>.Instance);

            var finishObject = new FinishObject { Id = 3, Name = "X" };

            ctx.FinishObjects.Add(new FinishObject { Id = 1, Name = "Erster" });
            ctx.FinishObjects.Add(new FinishObject { Id = 2, Name = "Zweiter" });
            ctx.FinishObjects.Add(finishObject);

            svc.DeleteFinishObject(finishObject);

            Assert.AreEqual(2, ctx.FinishObjects.Count());
        }

        [TestMethod]
        public void DeleteFinishObject_ThrowsException_WhenObjectNotFound()
        {
            using var ctx = CreateContext();
            var svc = new FinishService(ctx, NullLogger<FinishService>.Instance);

            var finishObject = new FinishObject { Id = 3, Name = "X" };

            ctx.FinishObjects.Add(new FinishObject { Id = 1, Name = "Erster" });
            ctx.FinishObjects.Add(new FinishObject { Id = 2, Name = "Zweiter" });

            Assert.ThrowsException<KeyNotFoundException>(() => svc.DeleteFinishObject(finishObject));
        }
    }
}