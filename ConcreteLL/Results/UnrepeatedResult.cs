using AbstractLL;

namespace ConcreteLL.Results
{
    public class UnrepeatedResult : AbstractResult
    {
        public Data.Variable Value { get; init; }

        public UnrepeatedResult(Data.Variable value)
        {
            Value = value;
        }
    }
}
