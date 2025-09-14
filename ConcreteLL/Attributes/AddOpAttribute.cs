using AbstractLL;

using ConcreteLL.Interfaces;
using ConcreteLL.Tokens;

namespace ConcreteLL.Attributes
{
    internal class AddOpAttribute : OperatorAttribute
    {
        public AddOpAttribute(AddOpToken @operator)
            : base(@operator)
        {
        }

        public AbstractAttribute Evaluate(AbstractAttribute left, AbstractAttribute right)
        {
            if (string.Compare(Operator, "+") == 0)
            {
                if (left is IAdditive equality)
                    return equality.Add(right);
            }
            else if (string.Compare(Operator, "-") == 0)
            {
                if (left is IAdditive equality)
                    return equality.Subtract(right);
            }

            throw new Exception($"Erro");
        }

        public override AbstractResult? ConvertToResult()
            => null;
    }
}
