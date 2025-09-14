using AbstractLL;

namespace ConcreteLL.Results
{
    public class VariableResult : AbstractResult
    {
        public Data.Variable Variable { get; init; }

        public VariableResult(Data.Variable variable)
        {
            Variable = variable;
        }
    }
}
