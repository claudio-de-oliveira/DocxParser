using AbstractLL;

namespace ConcreteLL.Attributes
{
    internal class LiteralAttribute : AbstractAttribute
    {
        public string Value { get; init; }

        public LiteralAttribute(string value)
        {
            Value = value;
        }

        public override AbstractResult? ConvertToResult()
        {
            throw new NotImplementedException();
        }
    }
}
