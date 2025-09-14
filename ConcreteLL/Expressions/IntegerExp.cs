namespace ConcreteLL.Expressions
{
    public class IntegerExp : AbsExpression
    {
        public long Value { get; set; }

        public IntegerExp(long value)
        {
            Value = value;
        }

        public override object Evaluate(Dictionary<string, ConcreteLL.Data.Variable> variables)
            => Value;

        public override string ToString()
            => $"{Value}";

        public override string ToXml()
            => $"<integer value=\"{Value}\"/>";
    }
}
