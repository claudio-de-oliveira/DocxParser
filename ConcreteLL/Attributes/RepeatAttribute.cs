using AbstractLL;

using ConcreteLL.Results;

namespace ConcreteLL.Attributes
{
    internal class RepeatAttribute : AbstractAttribute
    {
        public Data.Variable Value { get; init; }
        // public long Value { get; init; }

        public RepeatAttribute(Data.Variable value/*long value*/)
        {
            Value = value;
        }

        public override AbstractResult? ConvertToResult()
            => new RepeatResult(Value);
    }
}
