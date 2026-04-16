using LRV.Regatta.Buero.Models;

namespace LRV.Regatta.Buero.Interfaces
{
    /// <summary>
    /// IFinishService interface that defines the contract for managing FinishObject instances. This interface includes methods for adding, deleting, and retrieving finish line data, allowing for the implementation of various data storage solutions (e.g., in-memory, database) while maintaining a consistent API for handling finish line information in the context of a regatta management system.
    /// </summary>
    public interface IFinishService
    {
        /// <summary>
        /// Adds a FinishObject instance to the internal list of finish objects. This method allows for adding new finish line data to the in-memory collection, enabling the management of finish line information without the need for a persistent database.
        /// </summary>
        /// <param name="object">The FinishObject instance to be added to the in-memory collection.</param>
        void AddFinishObject(FinishObject @object);

        /// <summary>
        /// Deletes all FinishObject instances from the internal list. This method clears the in-memory collection, effectively removing all finish line data without affecting a persistent database.
        /// </summary>
        void DeleteAllFinishObject();

        /// <summary>
        /// Deletes a specific FinishObject instance from the internal list. This method removes the specified finish line data from the in-memory collection without affecting a persistent database.
        /// </summary>
        /// <param name="item">The FinishObject instance to be removed from the in-memory collection.</param>
        void DeleteFinishObject(FinishObject item);

        /// <summary>
        /// Retrieves all FinishObject instances from the internal list. This method returns the in-memory collection of finish line data, allowing for read operations without accessing a persistent database.
        /// </summary>
        /// <returns>A list of all FinishObject instances in the in-memory collection.</returns>
        IList<FinishObject> GetAllFinishObject();
    }
}
