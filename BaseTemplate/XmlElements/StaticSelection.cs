using System.Xml.Serialization;

using BaseTemplate.Base;

namespace BaseTemplate.XmlElements
{
    public class StaticSelection : AbstractXmlElement
    {
        [XmlText]
        public string? Value { get; set; }

        public override void ToXml()
        {
            Console.WriteLine($"        <StaticSelection>{Value}</StaticSelection>");
        }
    }

}
