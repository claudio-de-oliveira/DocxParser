namespace ConcreteLL.Expressions
{
    public class DecimalExp : AbsExpression
    {
        public double Value { get; set; }

        public DecimalExp(double value)
            => Value = value;

        public override object Evaluate(Dictionary<string, ConcreteLL.Data.Variable> variables)
            => Value;

        public override string ToString()
            => $"{Value}";

        public override string ToXml()
            => $"<decimal value=\"{Value}\"/>";
    }
}
