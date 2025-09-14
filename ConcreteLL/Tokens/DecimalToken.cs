using AbstractLL;

namespace ConcreteLL.Tokens
{
    internal class DecimalToken : AbstractToken
    {
        public double Value { get; private set; }

        public DecimalToken(double n)
            : base(Tag.DECIMAL)
        {
            Value = n;
        }

        public override bool HasComplement() => true;

        public override string ToString() => $"{GetTag()}: {Value}";
    }
}
