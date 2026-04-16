using LRV.Regatta.Buero.Models;
using LRV.Regatta.Buero.Services;
using LRV.Regatta.Buero.Tests.Helpers;
using Microsoft.Extensions.Logging.Abstractions;

namespace LRV.Regatta.Buero.Tests.Service
{
    [TestClass]
    public class RegistrationServiceTests
    {
        private DatabaseContext CreateContext() =>
            DatabaseContextFactory.Create($"RegistrationServiceTests_{Guid.NewGuid()}");

        [TestMethod]
        public void AddRegistration_PersistsItem()
        {
            using var ctx = CreateContext();
            var svc = new RegistrationService(ctx, NullLogger<RegistrationService>.Instance);

            svc.AddRegistration(new RegistrationObject
            {
                Id = 1,
                Type = RegistrationType.Registration,
                Race = "Race 1",
                StartNo = "1",
                Team = "Team 1",
                ChairMan = "ChairMan 1"
            });

            Assert.AreEqual(1, ctx.RegistrationObjects.Count());
        }

        [TestMethod]
        public void GetRegistrations_ReturnsAllItems()
        {
            using var ctx = CreateContext();
            ctx.RegistrationObjects.Add(new RegistrationObject
            {
                Id = 1,
                Type = RegistrationType.Registration,
                Race = "Race 1",
                StartNo = "1",
                Team = "Team 1",
                ChairMan = "ChairMan 1"
            });
            ctx.RegistrationObjects.Add(new RegistrationObject
            {
                Id = 2,
                Type = RegistrationType.LateRegistration,
                Race = "Race 2",
                StartNo = "2",
                Team = "Team 2",
                ChairMan = "ChairMan 2"
            });
            ctx.SaveChanges();

            var svc = new RegistrationService(ctx, NullLogger<RegistrationService>.Instance);

            var result = svc.GetRegistrations();

            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void DeleteRegistration_RemovesSpecificItem()
        {
            using var ctx = CreateContext();
            var svc = new RegistrationService(ctx, NullLogger<RegistrationService>.Instance);

            var registration = new RegistrationObject
            {
                Id = 2,
                Type = RegistrationType.LateRegistration,
                Race = "Race 2",
                StartNo = "2",
                Team = "Team 2",
                ChairMan = "ChairMan 2"
            };

            ctx.RegistrationObjects.Add(new RegistrationObject
            {
                Id = 1,
                Type = RegistrationType.Registration,
                Race = "Race 1",
                StartNo = "1",
                Team = "Team 1",
                ChairMan = "ChairMan 1"
            });
            ctx.RegistrationObjects.Add(registration);
            ctx.SaveChanges();

            svc.DeleteRegistration(registration);

            Assert.AreEqual(1, ctx.RegistrationObjects.Count());
            Assert.IsNull(ctx.RegistrationObjects.FirstOrDefault(r => r.Id == 2));
        }
    }
}