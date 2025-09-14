using AbstractLL;

using ConcreteLL.Results;

namespace ConcreteLL.Attributes
{
    internal class UnrepeatAttribute : AbstractAttribute
    {
        public Data.Variable Value { get; init; }
        // public long Value { get; init; }

        public UnrepeatAttribute(Data.Variable value/*long value*/)
        {
            Value = value;
        }

        public override AbstractResult? ConvertToResult()
            => new UnrepeatedResult(Value);
    }
}
