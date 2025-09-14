using ConcreteLL.Expressions;

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;

namespace TemplateBuilder.CustomElement
{
    public class ValueElement : OpenXmlCompositeElement
    {
        public override string NamespaceUri => "http://schemas.idealsoft.com/shop9/2025/expression";
        public override string Prefix => "s9";
        public override string LocalName => "value";

        public AbsExpression? Expression { get; set; }

        public override string InnerText => ToString();
        public override string ToString() => $"@{{{Expression?.ToXml() ?? ""}}}";

        public OpenXmlElement? CommonAncestral { get; set; }
        public OpenXmlElement? NewCommonAncestral { get; set; }
        public RunProperties? RunProperties { get; set; }

        public override OpenXmlElement CloneNode(bool deep)
        {
            var cloned = new ValueElement() { Expression = this.Expression, CommonAncestral = this.CommonAncestral };
            foreach (var child in this.ChildElements)
                cloned.Append(child.CloneNode(deep));
            return cloned;
        }
    }
}
