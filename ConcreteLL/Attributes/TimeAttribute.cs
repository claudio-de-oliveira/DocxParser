using AbstractLL;

using ConcreteLL.Results;

namespace ConcreteLL.Attributes
{
    internal class TimeAttribute : AbstractAttribute
    {
        public DateTime Value { get; init; }

        public TimeAttribute(DateTime value)
        {
            Value = value;
        }

        public override AbstractResult? ConvertToResult()
            => new DateResult(Value);
    }
}
