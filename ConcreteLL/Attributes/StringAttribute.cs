using AbstractLL;

using ConcreteLL.Results;

namespace ConcreteLL.Attributes
{
    internal class StringAttribute : AbstractAttribute
    {
        public string Value { get; init; }

        public StringAttribute(string value)
        {
            Value = value;
        }

        public override AbstractResult? ConvertToResult()
            => new StringResult(Value);
    }
}
