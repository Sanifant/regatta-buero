using System.Xml.Serialization;

namespace LRV.Regatta.Buero.Models
{
    public class TeamObject
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlElement("ort")]
        public string Ort { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("kurzform")]
        public string Kurzform { get; set; }

        [XmlElement("lettern")]
        public string Lettern { get; set; }
    }
}
