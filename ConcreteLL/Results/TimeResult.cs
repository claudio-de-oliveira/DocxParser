using AbstractLL;

namespace ConcreteLL.Results
{
    public class TimeResult : AbstractResult
    {
        public DateTime Value { get; init; }

        public TimeResult(DateTime value)
        {
            Value = value;
        }
    }
}
