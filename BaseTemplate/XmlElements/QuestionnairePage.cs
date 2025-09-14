using System.Xml.Serialization;

using BaseTemplate.Base;

namespace BaseTemplate.XmlElements
{
    public class QuestionnairePage : AbstractXmlElement
    {
        [XmlAttribute("ObjectID")]
        public string? ObjectID { get; set; }

        [XmlElement(ElementName = "Name")]
        public string? Name { get; set; }

        [XmlElement(ElementName = "QuestionnaireGroup")]
        public List<QuestionnaireGroup>? QuestionnaireGroups { get; set; }

        [XmlElement(ElementName = "Context")]
        public List<Context>? Contexts { get; set; }


        public QuestionnaireGroup? this[string objectID]
        {
            get { return QuestionnaireGroups?.FirstOrDefault(s => string.Equals(s.ObjectID, objectID, StringComparison.OrdinalIgnoreCase)); }
        }

        public override void ToXml()
        {
            Console.WriteLine($"    <QuestionnairePage ObjectID=\"{ObjectID}\">");
            Console.WriteLine($"        <Name>{Name}</Name>");
            foreach (var node in QuestionnaireGroups!)
                node.ToXml();
            foreach (var node in Contexts!)
                node.ToXml();
            Console.WriteLine("    </QuestionnairePage>");
        }
    }

}
