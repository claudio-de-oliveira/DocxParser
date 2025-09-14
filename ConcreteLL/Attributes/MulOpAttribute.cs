using AbstractLL;

using ConcreteLL.Interfaces;
using ConcreteLL.Tokens;

namespace ConcreteLL.Attributes
{
    internal class MulOpAttribute : OperatorAttribute
    {
        public MulOpAttribute(OperatorToken @operator)
            : base(@operator)
        {
        }

        public AbstractAttribute Evaluate(AbstractAttribute left, AbstractAttribute right)
        {
            if (string.Compare(Operator, "*") == 0)
            {
                if (left is IMultiplicative other)
                    return other.Multiply(right);
            }
            else if (string.Compare(Operator, "/") == 0)
            {
                if (left is IMultiplicative other)
                    return other.Divide(right);
            }
            else if (string.Compare(Operator, "//") == 0)
            {
                if (left is IMultiplicative other)
                    return other.IntegerDivide(right);
            }

            throw new Exception($"Erro");
        }

        public override AbstractResult? ConvertToResult()
            => null;
    }
}
