using LRV.Regatta.Buero.Models;

namespace LRV.Regatta.Buero.Services
{
    public interface IRegistrationService
    {
        void AddRegistration(RegistrationObject registration);
        List<RegistrationObject> GetRegistrations();
    }
}
