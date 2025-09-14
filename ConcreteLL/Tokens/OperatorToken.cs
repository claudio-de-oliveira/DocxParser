using AbstractLL;

namespace ConcreteLL.Tokens
{
    internal class OperatorToken : AbstractToken
    {
        public string Value { get; private set; }

        protected OperatorToken(Tag tag, string txt)
            : base(tag)
        {
            Value = txt;
        }

        public override bool HasComplement() => true;

        public override string ToString() => $"{GetTag()}: {Value}";
    }
}
