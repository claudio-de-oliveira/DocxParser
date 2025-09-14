using AbstractLL;

namespace ConcreteLL.Results
{
    public class BooleanResult : AbstractResult
    {
        public bool Value { get; init; }

        public BooleanResult(bool value)
        {
            Value = value;
        }
    }
}
