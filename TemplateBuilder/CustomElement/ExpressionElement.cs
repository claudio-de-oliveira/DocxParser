using ConcreteLL.Expressions;

using DocumentFormat.OpenXml;

namespace TemplateBuilder.CustomElement
{
    public class ExpressionElement : OpenXmlCompositeElement
    {
        public override string NamespaceUri => "http://schemas.idealsoft.com/shop9/2025/expression";
        public override string Prefix => "s9";
        public override string LocalName => "exp";

        public AbsExpression? Expression { get; set; }

        public override string InnerText => ToString();
        public override string ToString() => $"@[{Expression?.ToString() ?? ""}]";

        public OpenXmlElement? CommonAncestral { get; set; }
        public OpenXmlElement? NewCommonAncestral { get; set; }
        public OpenXmlElement? Content { get; set; }

        public Queue<OpenXmlElement> SavedList { get; set; } = [];

        public override OpenXmlElement CloneNode(bool deep)
        {
            var cloned = new ExpressionElement() { Expression = this.Expression, CommonAncestral = this.CommonAncestral };
            foreach (var child in this.ChildElements)
                cloned.Append(child.CloneNode(deep));
            return cloned;
        }
    }
}
