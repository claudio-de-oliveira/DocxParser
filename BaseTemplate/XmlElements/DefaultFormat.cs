using System.Xml;
using System.Xml.Serialization;

using BaseTemplate.Base;

namespace BaseTemplate.XmlElements
{
    public class DefaultFormat : AbstractXmlElement
    {
        [XmlText]
        public string? Value { get; set; }

        public override void ToXml()
        {
            Console.WriteLine($"        <DefaultFormat>{Value}</DefaultFormat>");
        }
    }
}
