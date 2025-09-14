using BaseTemplate.Base;

using DocumentFormat.OpenXml.Packaging;

using System.Xml;
using System.Xml.Serialization;

namespace BaseTemplate.XmlElements
{
    [Serializable, XmlRoot(ElementName = "Dictionary", Namespace = "http://schemas.business-integrity.com/dealbuilder/2006/dictionary")]
    public class Dictionary : AbstractXmlElement
    {
        [XmlAttribute("SavedByVersion")]
        public string? SavedByVersion { get; set; }
        [XmlAttribute("MinimumVersion")]
        public string? MinimumVersion { get; set; }
        [XmlElement(ElementName = "QuestionnairePage")]
        public List<QuestionnairePage> QuestionnairePages { get; set; }
        [XmlElement(ElementName = "Alert")]
        public Alert? Alert { get; set; }
        [XmlElement(ElementName = "Variable")]
        public List<Variable> Variables { get; set; }

        public Dictionary()
        {
            QuestionnairePages = [];
            Variables = [];
        }

        public QuestionnairePage? this[string objectID]
        {
            get { return QuestionnairePages.FirstOrDefault(s => string.Equals(s.ObjectID, objectID, StringComparison.OrdinalIgnoreCase)); }
        }

        public static AbstractXmlElement? ReadFromDocx(WordprocessingDocument mainDocument)
        {
            var parts = mainDocument.GetAllParts();
            AbstractXmlElement? dictionary = null;

            foreach (var part in parts)
            {
                if (part is CustomXmlPart custom)
                {
                    StreamReader reader = new(part.GetStream(FileMode.Open, FileAccess.Read));
                    string fullXML = reader.ReadToEnd();

                    var xmlSerializer = new XmlSerializer(typeof(Dictionary));

                    using var sr = new StringReader(fullXML);
                    dictionary = (AbstractXmlElement?)xmlSerializer.Deserialize(sr);
                }
            }
            return dictionary;
        }

        public override void ToXml()
        {
            Console.WriteLine($"<Dictionary SavedByVersion=\"{SavedByVersion}\" MinimumVersion=\"{MinimumVersion}\" ");
            Console.WriteLine("xmlns=\"http://schemas.business-integrity.com/dealbuilder/2006/dictionary\">");
            foreach (var node in QuestionnairePages)
                node.ToXml();
            Alert?.ToXml();
            foreach (var node in Variables)
                node.ToXml();
            Console.WriteLine("</Dictionary>");
        }
    }
}
