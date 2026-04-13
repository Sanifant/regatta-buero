using Microsoft.EntityFrameworkCore;
using LRV.Regatta.Buero.Models;

namespace LRV.Regatta.Buero.Services
{
    /// <summary>
    /// DatabaseContext class for Entity Framework Core, representing the database context for the application. It includes DbSet properties for FinishObjects, RegistrationObjects, TeamObjects, and LogObjects, allowing for CRUD operations on these entities in the database.
    /// </summary>
    public class DatabaseContext : DbContext
    {
        /// <summary>
        /// Constructor for the DatabaseContext class, accepting DbContextOptions and passing them to the base DbContext constructor. This allows for configuration of the database connection and other options when initializing the context.
        /// </summary>
        /// <param name="options"></param>
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        /// <summary>
        /// DbSet properties for the entities in the database. Each DbSet represents a table in the database and allows for querying and saving instances of the corresponding entity type. The FinishObjects DbSet represents the finish line data, RegistrationObjects represents the registration data, TeamObjects represents the team data, and LogObjects represents the log data. These properties enable CRUD operations on the respective entities in the database.
        /// </summary>
        public DbSet<FinishObject> FinishObjects { get; set; }

        /// <summary>
        /// DbSet property for RegistrationObjects, representing the registration data in the database. This allows for querying and saving instances of RegistrationObject entities, enabling CRUD operations on the registration data in the database.
        /// </summary>
        public DbSet<RegistrationObject> RegistrationObjects { get; set; }

        /// <summary>
        /// DbSet property for TeamObjects, representing the team data in the database. This allows for querying and saving instances of TeamObject entities, enabling CRUD operations on the team data in the database.
        /// </summary>
        public DbSet<TeamObject> TeamObjects { get; set; }

        /// <summary>
        /// DbSet property for LogObjects, representing the log data in the database. This allows for querying and saving instances of LogObject entities, enabling CRUD operations on the log data in the database.
        /// </summary>
        public DbSet<LogObject> LogObjects { get; set; }
    }
}