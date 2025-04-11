using LRV.Regatta.Buero.Models;

namespace LRV.Regatta.Buero.Services
{
    public class MemoryDataService : IRegistrationService
    {
        List<RegistrationObject> memory;

        public MemoryDataService() {
        
            this.memory = new List<RegistrationObject>();


            for (int i = 0; i < 15; i++)
            {
                memory.Add(new RegistrationObject()
                {
                    Type = i % 2 == 0 ? RegistrationType.Registration : RegistrationType.LateRegistration,
                    Race = $"Race {i}",
                    StartNo = $"Race {i}",
                    Team = $"Race {i}",
                    ChairMan = $"Race {i}"
                });
            }

        }

        public void AddRegistration(RegistrationObject registration)
        {
            this.memory.Add(registration);
        }

        public List<RegistrationObject> GetRegistrations()
        {
            return this.memory;
        }
    }
}
