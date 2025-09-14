using AbstractLL;

namespace ConcreteLL.Results
{
    public class IntegerResult : AbstractResult
    {
        public long Value { get; init; }

        public IntegerResult(long value)
        {
            Value = value;
        }
    }
}
