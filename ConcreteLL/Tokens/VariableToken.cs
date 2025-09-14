using AbstractLL;

namespace ConcreteLL.Tokens
{
    internal class VariableToken : AbstractToken
    {
        public string Value { get; private set; }

        public VariableToken(string value)
            : base(Tag.VARIABLE)
        {
            Value = value;
        }

        public override bool HasComplement() => true;

        public override string ToString() => $"{GetTag()}: {Value}";
    }
}
