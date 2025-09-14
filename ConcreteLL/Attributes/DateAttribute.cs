using AbstractLL;

using ConcreteLL.Results;

namespace ConcreteLL.Attributes
{
    internal class DateAttribute : AbstractAttribute
    {
        public DateTime Value { get; init; }

        public DateAttribute(DateTime value)
        {
            Value = value;
        }

        public override AbstractResult? ConvertToResult()
            => new DateResult(Value);
    }
}
