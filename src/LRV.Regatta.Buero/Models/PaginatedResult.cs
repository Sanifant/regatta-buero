namespace LRV.Regatta.Buero.Models
{
    /// <summary>
    /// Represents a paginated result containing a list of items and the total count of items in the database.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedResult<T>
    {
        /// <summary>
        /// Gets or sets the list of items for the current page.
        /// </summary>
        public List<T> Items { get; set; } = new();

        /// <summary>
        /// Gets or sets the total count of items in the database.
        /// </summary>
        public int TotalCount { get; set; }
    }
}
