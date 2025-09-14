using AbstractLL;

namespace ConcreteLL.Results
{
    public class DateResult : AbstractResult
    {
        public DateTime Value { get; init; }

        public DateResult(DateTime value)
        {
            Value = value;
        }
    }
}
