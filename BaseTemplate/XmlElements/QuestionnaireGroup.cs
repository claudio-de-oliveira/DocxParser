using System.Xml.Serialization;

using BaseTemplate.Base;

namespace BaseTemplate.XmlElements
{
    public class QuestionnaireGroup : AbstractXmlElement
    {
        [XmlAttribute("ObjectID")]
        public string? ObjectID { get; set; }
        [XmlElement(ElementName = "Name")]
        public string? Name { get; set; }
        [XmlElement(ElementName = "QuestionnaireVariable")]
        public List<QuestionnaireVariable> QuestionnaireVariables { get; set; }
        [XmlElement(ElementName = "Context")]
        public List<Context>? Contexts { get; set; }

        public QuestionnaireGroup()
        {
            QuestionnaireVariables = [];
        }

        public QuestionnaireVariable? this[string name]
        {
            get { return QuestionnaireVariables.FirstOrDefault(s => string.Equals(s.Name, name, StringComparison.OrdinalIgnoreCase)); }
        }

        public override void ToXml()
        {
            Console.WriteLine($"        <QuestionnaireGroup ObjectID=\"{ObjectID}\">");
            Console.WriteLine($"            <Name>{Name}</Name>");
            foreach (var node in QuestionnaireVariables)
                node.ToXml();
            foreach (var node in Contexts!)
                node.ToXml();
            Console.WriteLine($"        </QuestionnaireGroup>");
        }
    }

}
