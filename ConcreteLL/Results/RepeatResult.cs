using AbstractLL;

namespace ConcreteLL.Results
{
    public class RepeatResult : AbstractResult
    {
        public Data.Variable Value { get; init; }

        public RepeatResult(Data.Variable value)
        {
            Value = value;
        }
    }
}
