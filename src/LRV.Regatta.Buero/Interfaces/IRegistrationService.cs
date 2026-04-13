using LRV.Regatta.Buero.Models;

namespace LRV.Regatta.Buero.Interfaces
{
    /// <summary>
    /// IRegistrationService interface that defines the contract for managing RegistrationObject instances. This interface includes methods for adding and retrieving registration data, allowing for the implementation of various data storage solutions (e.g., in-memory, database) while maintaining a consistent API for handling registration information in the context of a regatta management system.
    /// </summary>
    public interface IRegistrationService
    {
        /// <summary>
        /// Adds a RegistrationObject instance to the internal list of registration objects. This method allows for adding new registration data to the in-memory collection, enabling the management of registration information without the need for a persistent database.
        /// </summary>
        /// <param name="registration">The RegistrationObject instance to be added to the in-memory collection.</param>
        void AddRegistration(RegistrationObject registration);

        /// <summary>
        /// Retrieves all RegistrationObject instances from the internal list. This method returns the in-memory collection of registration data, allowing for read operations without accessing a persistent database.
        /// </summary>
        /// <returns>A list of all RegistrationObject instances in the in-memory collection.</returns>
        List<RegistrationObject> GetRegistrations();
    }
}
