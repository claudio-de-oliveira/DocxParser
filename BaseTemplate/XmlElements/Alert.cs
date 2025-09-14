using BaseTemplate.Base;

using System.Xml;
using System.Xml.Serialization;

namespace BaseTemplate.XmlElements
{
    public class Alert : AbstractXmlElement
    {
        [XmlAttribute("ObjectID")]
        public string? ObjectID { get; set; }
        [XmlAttribute("Compulsory")]
        public bool Compulsory { get; set; }
        [XmlElement("Definition")]
        public Definition? Definition { get; set; }
        [XmlElement("Message")]
        public Message? Message { get; set; }

        public override void ToXml()
        {
            Console.WriteLine($"    <Alert ObjectID=\"{ObjectID}\" Compulsory=\"{Compulsory}\">");
            Definition?.ToXml();
            Message?.ToXml();
            Console.WriteLine("    </Alert>");
        }
    }
}
