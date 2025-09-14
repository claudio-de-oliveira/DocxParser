using AbstractLL;

using ConcreteLL.Results;

namespace ConcreteLL.Attributes
{
    internal class TextAttribute : AbstractAttribute
    {
        public string Value { get; init; }

        public TextAttribute(string value)
        {
            Value = value;
        }

        public BooleanAttribute Is(LiteralAttribute right)
            => new(string.Compare(Value, right.Value) == 0);
        public BooleanAttribute IsNot(LiteralAttribute right)
            => new(string.Compare(Value, right.Value) != 0);
        public BooleanAttribute IsLessThan(LiteralAttribute right)
            => new(string.Compare(Value, right.Value) < 0);
        public BooleanAttribute IsAtMost(LiteralAttribute right)
            => new(string.Compare(Value, right.Value) <= 0);
        public BooleanAttribute IsMoreThan(LiteralAttribute right)
            => new(string.Compare(Value, right.Value) > 0);
        public BooleanAttribute IsAtLeast(LiteralAttribute right)
            => new(string.Compare(Value, right.Value) >= 0);

        public override AbstractResult? ConvertToResult()
            => new StringResult(Value);
    }
}
