using AbstractLL;

namespace ConcreteLL.Tokens
{
    internal class LiteralToken : AbstractToken
    {
        public string Value { get; private set; }

        public LiteralToken(string txt)
            : base(Tag.LITERAL)
        {
            Value = txt;
        }

        public override bool HasComplement() => true;

        public override string ToString() => $"{GetTag()}: {Value}";
    }
}
