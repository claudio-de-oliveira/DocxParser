using System.Xml.Serialization;

using BaseTemplate.Base;

namespace BaseTemplate.XmlElements
{
    public class QuestionnaireVariable : AbstractXmlElement
    {
        [XmlAttribute("Name")]
        public string? Name { get; set; }

        public override void ToXml()
        {
            Console.WriteLine($"            <QuestionnaireVariable Name =\"{Name}\"/>");
        }
    }

}
