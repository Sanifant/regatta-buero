using LRV.Regatta.Buero.Models;
using LRV.Regatta.Buero.Interfaces;

namespace LRV.Regatta.Buero.Services
{
    /// <summary>
    /// Service class for managing registration operations in the regatta management system.
    /// </summary>
    public class RegistrationService : IRegistrationService
    {
        private readonly DatabaseContext databaseContext;
        private readonly ILogger<RegistrationService> logger;

        /// <summary>
        /// Constructor for the RegistrationService class, initializing the DatabaseContext dependency.
        /// </summary>
        /// <param name="context">The DatabaseContext instance used for accessing the database.</param>
        /// <param name="logger">The ILogger instance used for logging.</param>
        public RegistrationService(DatabaseContext context, ILogger<RegistrationService> logger)
        {
            this.databaseContext = context;
            this.logger = logger;
        }

        /// <summary>
        /// Adds a new registration to the database.
        /// </summary>
        /// <param name="registration">The registration object to be added.</param>
        public void AddRegistration(RegistrationObject registration)
        {
            this.databaseContext.Add(registration);
            this.databaseContext.SaveChanges();
        }

        /// <summary>
        /// Retrieves all registrations from the database.
        /// </summary>
        /// <returns>A list of all registration objects.</returns>
        public List<RegistrationObject> GetRegistrations()
        {
            return this.databaseContext.RegistrationObjects.ToList();
        }

        /// <summary>
        /// Retrieves a registration by its ID from the database.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown when the ID is less than or equal to zero.</exception>
        /// <exception cref="InvalidOperationException">Thrown when no registration is found with the specified ID.</exception>
        /// <param name="Id">The ID of the registration to retrieve.</param>
        /// <returns>The registration object with the specified ID.</returns>
        public RegistrationObject GetRegistration(int id)
        {
            if(id <= 0)
            {
                this.logger.LogError("Invalid ID: {Id}. ID must be greater than zero.", id);
                throw new ArgumentException("ID must be greater than zero.", nameof(id));
            }

            var registration = this.databaseContext.RegistrationObjects.FirstOrDefault(r => r.Id == id);

            if(registration == null)
            {
                this.logger.LogError("No registration found with ID {Id}.", id);
                throw new InvalidOperationException($"No registration found with ID {id}.");
            }

            return registration;
        }

        /// <summary>
        /// Deletes a registration from the database.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown when the registration parameter is null.</exception>
        /// <param name="registration">The registration object to be deleted.</param>
        public void DeleteRegistration(RegistrationObject registration)
        {
            if(registration == null)
            {
                this.logger.LogError("Attempted to delete a null registration.");
                throw new ArgumentNullException(nameof(registration), "Registration cannot be null.");
            }


            this.databaseContext.Remove(registration);
            this.databaseContext.SaveChanges();
        }
        
    }
}