using System.Xml.Serialization;
using System;

namespace LRV.Regatta.Buero.Models
{
    [XmlRoot(ElementName = "regatta-meldungen", Namespace = "http://schemas.rudern.de/service/meldungen/2010/")]
    public class RegattaMeldungen
    {
        [XmlArray("vereine", Namespace = "")]
        [XmlArrayItem("verein")]
        public List<TeamObject> Vereine { get; set; }
    }
}
