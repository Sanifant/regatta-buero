using LRV.Regatta.Buero.Interfaces;
using LRV.Regatta.Buero.Models;

namespace LRV.Regatta.Buero.Tests.Mocks
{
    internal class MockRegistrationService : IRegistrationService
    {
        public List<RegistrationObject> Registrations { get; set; } = new List<RegistrationObject>();

        public void AddRegistration(RegistrationObject registration)
        {
            Registrations.Add(registration);
        }

        public void DeleteRegistration(RegistrationObject registration)
        {
            throw new NotImplementedException();
        }

        public RegistrationObject GetRegistration(int Id)
        {
            throw new NotImplementedException();
        }

        public List<RegistrationObject> GetRegistrations()
        {
            return Registrations;
        }
    }
}
