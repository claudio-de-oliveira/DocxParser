using System.Xml;
using System.Xml.Serialization;

using BaseTemplate.Base;

namespace BaseTemplate.XmlElements
{
    public class Guidance : AbstractXmlElement
    {
        [XmlText]
        public string? Value { get; set; }

        public override void ToXml()
        {
            Console.WriteLine($"        <Guidance>{Value}</Guidance>");
        }
    }
}
