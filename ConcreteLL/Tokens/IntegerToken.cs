using AbstractLL;

namespace ConcreteLL.Tokens
{
    internal class IntegerToken : AbstractToken
    {
        public long Value { get; private set; }

        public IntegerToken(long n)
            : base(Tag.INTEGER)
        {
            Value = n;
        }

        public override bool HasComplement() => true;

        public override string ToString() => $"{GetTag()}: {Value}";
    }
}
