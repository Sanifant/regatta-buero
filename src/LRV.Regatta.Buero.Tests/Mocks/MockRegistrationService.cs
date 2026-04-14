using LRV.Regatta.Buero.Interfaces;
using LRV.Regatta.Buero.Models;
using LRV.Regatta.Buero.Services;
using LRV.Regatta.Buero.Tests.Helpers;
using Microsoft.Extensions.Logging.Abstractions;

namespace LRV.Regatta.Buero.Tests.Mocks
{
    internal sealed class MockRegistrationService : IRegistrationService, IDisposable
    {
        private readonly DatabaseContext databaseContext;
        private readonly RegistrationService registrationService;

        public MockRegistrationService()
        {
            this.databaseContext = DatabaseContextFactory.Create($"MockRegistrationService_{Guid.NewGuid()}");
            this.registrationService = new RegistrationService(this.databaseContext, NullLogger<RegistrationService>.Instance);
        }

        public List<RegistrationObject> Registrations => this.registrationService.GetRegistrations();

        public void Seed(params RegistrationObject[] registrations)
        {
            this.databaseContext.RegistrationObjects.AddRange(registrations);
            this.databaseContext.SaveChanges();
        }

        public void AddRegistration(RegistrationObject registration)
        {
            this.registrationService.AddRegistration(registration);
        }

        public void DeleteRegistration(RegistrationObject registration)
        {
            this.registrationService.DeleteRegistration(registration);
        }

        public RegistrationObject GetRegistration(int Id)
        {
            return this.registrationService.GetRegistration(Id);
        }

        public List<RegistrationObject> GetRegistrations()
        {
            return this.registrationService.GetRegistrations();
        }

        public void Dispose()
        {
            this.databaseContext.Dispose();
        }
    }
}
