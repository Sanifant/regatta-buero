using LRV.Regatta.Buero.Services;
using Microsoft.EntityFrameworkCore;

namespace LRV.Regatta.Buero.Tests.Helpers
{
    internal static class DatabaseContextFactory
    {
        // Jeder Aufruf mit einem eindeutigen Namen liefert eine isolierte, leere DB
        public static DatabaseContext Create(string dbName)
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(dbName)
                .Options;

            return new DatabaseContext(options);
        }
    }
}