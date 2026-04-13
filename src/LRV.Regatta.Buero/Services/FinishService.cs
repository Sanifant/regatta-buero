using LRV.Regatta.Buero.Models;
using LRV.Regatta.Buero.Interfaces;

namespace LRV.Regatta.Buero.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class FinishService : IFinishService
    {
        private readonly DatabaseContext databaseContext;
        private readonly ILogger<FinishService> logger;

        /// <summary>
        /// Constructor for the MysqlDataService class, initializing the DatabaseContext dependency.
        /// </summary>
        /// <param name="context">The DatabaseContext instance used for accessing the database.</param>
        /// <param name="logger">The ILogger instance used for logging.</param>
        public FinishService(DatabaseContext context, ILogger<FinishService> logger)
        {
            this.databaseContext = context;
            this.logger = logger;
        }

        /// <summary>
        /// Adds a new finish object to the database.
        /// </summary>
        /// <param name="object">The finish object to add.</param>
        public void AddFinishObject(FinishObject @object)
        {
            this.databaseContext.FinishObjects.Add(@object);
            this.databaseContext.SaveChanges();
        }

        /// <summary>
        /// Retrieves all finish objects from the database, ordered by their ID in descending order.
        /// </summary>
        /// <returns>A list of all finish objects.</returns>
        public IList<FinishObject> GetAllFinishObject()
        {
            return this.databaseContext.FinishObjects.OrderByDescending(r => r.Id).ToList();
        }

        /// <summary>
        /// Deletes all finish objects from the database.
        /// </summary>
        public void DeleteAllFinishObject()
        {
            this.databaseContext.FinishObjects.RemoveRange(this.databaseContext.FinishObjects);
            this.databaseContext.SaveChanges();
        }

        /// <summary>
        /// Deletes a specific finish object from the database.
        /// </summary>
        /// <param name="object">The finish object to delete.</param>
        public void DeleteFinishObject(FinishObject @object)
        {
            this.databaseContext.FinishObjects.Remove(@object);
            this.databaseContext.SaveChanges();
        }
    }
}
