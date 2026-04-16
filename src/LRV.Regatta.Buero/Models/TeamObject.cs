using System.Xml.Serialization;

namespace LRV.Regatta.Buero.Models
{
    /// <summary>
    /// TeamObject class representing the data structure for team information in the regatta management system. This class includes properties for Id, Ort, Name, Kurzform, and Lettern, allowing for the storage and retrieval of team data in the application. The TeamObject can be used to manage and analyze team information effectively in the context of a regatta management system.
    /// </summary>
    public class TeamObject
    {
        /// <summary>
        /// Gets or sets the unique identifier for the team object. This property is decorated with the [XmlAttribute] attribute, indicating that it should be serialized as an XML attribute with the specified name. The Id property allows for the storage and retrieval of the unique identifier associated with the team, enabling the application to manage and analyze team information effectively in the context of a regatta management system.
        /// </summary>
        [XmlAttribute("id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the location (Ort) for the team object. This property is decorated with the [XmlElement] attribute, indicating that it should be serialized as an XML element with the specified name. The Ort property allows for the storage and retrieval of the location associated with the team, enabling the application to manage and analyze team information effectively in the context of a regatta management system.
        /// </summary>
        [XmlElement("ort")]
        public string Ort { get; set; }

        /// <summary>
        /// Gets or sets the name for the team object. This property is decorated with the [XmlElement] attribute, indicating that it should be serialized as an XML element with the specified name. The Name property allows for the storage and retrieval of the name associated with the team, enabling the application to manage and analyze team information effectively in the context of a regatta management system.
        /// </summary>
        [XmlElement("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the short form (Kurzform) for the team object. This property is decorated with the [XmlElement] attribute, indicating that it should be serialized as an XML element with the specified name. The Kurzform property allows for the storage and retrieval of the short form associated with the team, enabling the application to manage and analyze team information effectively in the context of a regatta management system.
        /// </summary>
        [XmlElement("kurzform")]
        public string Kurzform { get; set; }

        /// <summary>
        /// Gets or sets the letters (Lettern) for the team object. This property is decorated with the [XmlElement] attribute, indicating that it should be serialized as an XML element with the specified name. The Lettern property allows for the storage and retrieval of the letters associated with the team, enabling the application to manage and analyze team information effectively in the context of a regatta management system.
        /// </summary>
        [XmlElement("lettern")]
        public string Lettern { get; set; }
    }
}
