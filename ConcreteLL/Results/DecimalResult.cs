using AbstractLL;

namespace ConcreteLL.Results
{
    public class DecimalResult : AbstractResult
    {
        public double Value { get; init; }

        public DecimalResult(double value)
        {
            Value = value;
        }
    }
}
