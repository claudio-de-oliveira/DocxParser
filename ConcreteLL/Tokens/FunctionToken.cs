using AbstractLL;

namespace ConcreteLL.Tokens
{
    internal class FunctionToken : AbstractToken
    {
        public string Value { get; private set; }
        public string Description { get; private set; }

        public FunctionToken(string fx, string description = "")
            : base(Tag.FUNCTION)
        {
            Value = fx;
            Description = description;
        }

        public override bool HasComplement() => true;

        public override string ToString() => $"{GetTag()}: {Value}";
    }
}
