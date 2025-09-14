using AbstractLL;

namespace ConcreteLL.Results
{
    public class StringResult : AbstractResult
    {
        public string Value { get; init; }

        public StringResult(string value)
        {
            Value = value;
        }
    }
}
