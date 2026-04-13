using LRV.Regatta.Buero.Models;
using LRV.Regatta.Buero.Interfaces;

namespace LRV.Regatta.Buero.Services
{
    /// <summary>
    /// MemoryDataService class that implements the IRegistrationService interface, providing an in-memory implementation for managing RegistrationObject instances. This service allows for adding and retrieving registration data without the need for a persistent database, making it suitable for testing and development purposes.
    /// </summary>
    public class MemoryDataService : IRegistrationService
    {
        List<RegistrationObject> memory;

        /// <summary>
        /// Constructor for the MemoryDataService class, initializing the internal list of RegistrationObject instances. This constructor sets up an empty list to store RegistrationObject instances in memory, allowing for operations such as adding and retrieving registration data without relying on a database.
        /// </summary>
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
        
        /// <summary>
        /// Adds a RegistrationObject instance to the internal list of registration objects. This method allows for adding new registration data to the in-memory collection, enabling the management of registration information without the need for a persistent database.
        /// </summary>
        /// <param name="registration">The RegistrationObject instance to be added to the in-memory collection.</param>
        public void AddRegistration(RegistrationObject registration)
        {
            this.memory.Add(registration);
        }
        
        /// <summary>
        /// Retrieves all RegistrationObject instances from the internal list. This method returns the in-memory collection of registration data, allowing for read operations without accessing a persistent database.
        /// </summary>        
        /// <returns>A list of all RegistrationObject instances in the in-memory collection.</returns>
        public List<RegistrationObject> GetRegistrations()
        {
            return this.memory;
        }
    }
}
