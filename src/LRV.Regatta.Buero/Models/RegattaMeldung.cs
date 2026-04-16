using System.Xml.Serialization;

namespace LRV.Regatta.Buero.Models
{
    /// <summary>
    /// RegattaMeldungen class representing the data structure for regatta registrations in the regatta management system. This class includes a property for Vereine, which is a list of TeamObject instances, allowing for the storage and retrieval of team information in the application. The RegattaMeldungen class can be used to manage and analyze regatta registrations effectively in the context of a regatta management system.
    /// </summary>
    [XmlRoot(ElementName = "regatta-meldungen", Namespace = "http://schemas.rudern.de/service/meldungen/2010/")]
    public class RegattaMeldungen
    {
        /// <summary>
        /// Gets or sets the list of TeamObject instances representing the teams registered for the regatta. This property is decorated with the [XmlArray] and [XmlArrayItem] attributes, indicating that it should be serialized as an XML array with the specified element names. The Vereine property allows for the storage and retrieval of team information in the application, enabling the management and analysis of regatta registrations effectively in the context of a regatta management system.
        /// </summary>
        [XmlArray("vereine", Namespace = "")]
        [XmlArrayItem("verein")]
        public List<TeamObject> Vereine { get; set; }
    }
}
