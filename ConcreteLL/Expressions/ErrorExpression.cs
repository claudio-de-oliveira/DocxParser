namespace ConcreteLL.Expressions
{
    public class ErrorExpression : AbsExpression
    {
        public string Value { get; set; }

        public ErrorExpression(string value)
        {
            Value = value;
        }

        public override object Evaluate(Dictionary<string, ConcreteLL.Data.Variable> variables)
            => Value;

        public override string ToString()
            => $"Error: {Value}";

        public override string ToXml()
            => $"<error message=\"{Value}\"/>";
    }
}
