using LRV.Regatta.Buero.Interfaces;
using LRV.Regatta.Buero.Models;
using LRV.Regatta.Buero.Tests.Mocks;

namespace LRV.Regatta.Buero.Controllers.Tests
{
    [TestClass()]
    public class RegistrationControllerTests
    {
        private MockRegistrationService _registrationService = null!;

        [TestInitialize]
        public void TestInitialize()
        {
            _registrationService = new MockRegistrationService();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _registrationService.Dispose();
        }

        [TestMethod()]
        public void Test_That_Get()
        {
            var controller = new RegistrationController(_registrationService);
            _registrationService.Seed(
                Enumerable.Range(0, 15)
                    .Select(i => new RegistrationObject()
                    {
                        Type = i % 2 == 0 ? RegistrationType.Registration : RegistrationType.LateRegistration,
                        Race = $"Race {i}",
                        StartNo = $"Race {i}",
                        Team = $"Race {i}",
                        ChairMan = $"Race {i}"
                    })
                    .ToArray());

            var returnValue = controller.Get();

            Assert.AreEqual(15, returnValue.Count);
        }

        [TestMethod()]
        public void Test_That_AddRegistration()
        {
            var controller = new RegistrationController(_registrationService);
            var registration = new RegistrationObject()
            {
                Type = RegistrationType.Registration,
                Race = "Race 1",
                StartNo = "1",
                Team = "Team 1",
                ChairMan = "ChairMan 1"
            };

            controller.AddRegistration(registration);
            
            var returnValue = _registrationService.Registrations.FirstOrDefault();
            Assert.IsNotNull(returnValue);
            Assert.AreEqual(registration.Type, returnValue.Type);
            Assert.AreEqual(registration.Race, returnValue.Race);
            Assert.AreEqual(registration.StartNo, returnValue.StartNo);
            Assert.AreEqual(registration.Team, returnValue.Team);
            Assert.AreEqual(registration.ChairMan, returnValue.ChairMan);
        }
    }
}