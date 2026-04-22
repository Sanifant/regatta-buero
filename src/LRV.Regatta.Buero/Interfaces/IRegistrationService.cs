using LRV.Regatta.Buero.Models;

namespace LRV.Regatta.Buero.Interfaces
{
    /// <summary>
    /// IRegistrationService interface that defines the contract for managing RegistrationObject instances. This interface includes methods for adding and retrieving registration data, allowing for the implementation of various data storage solutions (e.g., in-memory, database) while maintaining a consistent API for handling registration information in the context of a regatta management system.
    /// </summary>
    public interface IRegistrationService
    {
        /// <summary>
        /// Adds a RegistrationObject instance to the internal list of registration objects.
        /// </summary>
        /// <remarks>
        /// The implementation of this method should ensure that the provided RegistrationObject instance is 
        /// valid and properly added to the internal collection, allowing for subsequent retrieval and management of 
        /// registration data within the application.
        /// </remarks>
        /// <param name="registration">The RegistrationObject instance to be added to the in-memory collection.</param>
        void AddRegistration(RegistrationObject registration);

        /// <summary>
        /// Retrieves all RegistrationObject instances from the internal list.
        /// </summary>
        /// <remarks>
        /// The implementation of this method should return a list of all RegistrationObject instances currently 
        /// stored in the internal collection, allowing for the management and analysis of registration data 
        /// within the application.
        /// </remarks>
        /// <returns>A list of all RegistrationObject instances in the in-memory collection.</returns>
        List<RegistrationObject> GetRegistrations();

        /// <summary>
        /// Retrieves a RegistrationObject instance by its ID from the internal list. 
        /// </summary>
        /// <remarks>
        /// The implementation of this method should search the internal collection for a RegistrationObject instance
        /// with the specified ID and return it if found. If no instance with the given ID exists, the method should 
        /// return null or throw an appropriate exception, depending on the design of the application.
        /// </remarks>
        /// <param name="id">The unique identifier of the RegistrationObject instance to be retrieved.</param>
        /// <returns>The RegistrationObject instance with the specified ID, or null if not found.</returns>
        RegistrationObject GetRegistration(int id);

        /// <summary>
        /// Deletes a registration from the database.
        /// </summary>
        /// <remarks>
        /// The implementation of this method should ensure that the provided RegistrationObject instance is valid and 
        /// properly removed from the internal collection, allowing for the management of registration data within the application. 
        /// If the provided RegistrationObject instance does not exist in the internal collection, the method 
        /// should handle this case appropriately, either by ignoring the request or by throwing an exception.
        /// </remarks>
        /// <exception cref="ArgumentNullException">Thrown when the registration parameter is null.</exception>
        /// <param name="registration">The registration object to be deleted.</param>
        void DeleteRegistration(RegistrationObject registration);
    }
}
