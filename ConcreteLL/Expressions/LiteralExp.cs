namespace ConcreteLL.Expressions
{
    public class LiteralExp : AbsExpression
    {
        public string Value { get; set; }

        public LiteralExp(string value)
        {
            Value = value;
        }

        public override object Evaluate(Dictionary<string, ConcreteLL.Data.Variable> variables)
            => Value;

        public override string ToString()
            => $"&quot;{Value}&quot;";

        public override string ToXml()
            => $"<literal value=\"&quot;{Value}&quot;\"/>";
    }
}
