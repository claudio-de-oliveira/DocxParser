using System.Xml.Serialization;

using BaseTemplate.Base;

namespace BaseTemplate.XmlElements
{
    public class Variable : AbstractXmlElement
    {
        [XmlAttribute("OtherOption")]
        public bool OtherOption { get; set; }
        [XmlAttribute("InputMethod")]
        public string? InputMethod { get; set; }
        [XmlAttribute("Name")]
        public string? Name { get; set; }
        [XmlAttribute("DataType")]
        public string? DataType { get; set; }
        [XmlAttribute("FieldOnly")]
        public bool FieldOnly { get; set; }
        [XmlAttribute("OccursOrder")]
        public int OccursOrder { get; set; }
        [XmlElement("Context")]
        public Context? Context { get; set; }
        [XmlElement("Prompt")]
        public Prompt? Prompt { get; set; }
        [XmlElement("Repeat")]
        public object? Repeat { get; set; }
        [XmlElement("Repeats")]
        public int Repeats { get; set; }
        [XmlElement("Value")]
        public string[]? Value { get; set; }
        [XmlElement("StaticSelection")]
        public List<StaticSelection>? StaticSelections { get; set; }
        [XmlElement("Guidance")]
        public Guidance? Guidance { get; set; }
        [XmlElement("SelectionValues")]
        public string[]? SelectionValues { get; set; }
        [XmlElement("Definition")]
        public string? Definition { get; set; }

        public override void ToXml()
        {
            Console.WriteLine($"    <Variable InputMethod=\"{InputMethod}\" Name=\"{Name}\" DataType=\"{DataType}\" FieldOnly=\"{FieldOnly}\" OccursOrder=\"{OccursOrder}\">");
            Prompt?.ToXml();
            Guidance?.ToXml();
            foreach (var node in StaticSelections!)
                node.ToXml();
            Context?.ToXml();
            Console.WriteLine("    </Variable>");
        }
    }
}
