namespace LRV.Regatta.Buero.Models
{
    /// <summary>
    /// FinishObject class representing the data structure for finish line information in the regatta management system. This class includes properties for Id, Name, FirstPath, and SecondPath, allowing for the storage and retrieval of finish line data in the application. The FinishObject can be used to manage and analyze finish line
    /// </summary>
    public class FinishObject
    {
        /// <summary>
        /// Gets or sets the unique identifier for the finish object.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the finish object.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the first path of the finish object.
        /// </summary>
        public string FirstPath { get; set; }

        /// <summary>
        /// Gets or sets the second path of the finish object.
        /// </summary>
        public string SecondPath { get; set; }
    }
}
