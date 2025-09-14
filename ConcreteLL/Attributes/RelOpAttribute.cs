using AbstractLL;

using ConcreteLL.Interfaces;
using ConcreteLL.Tokens;

namespace ConcreteLL.Attributes
{
    internal class RelOpAttribute : OperatorAttribute
    {
        public RelOpAttribute(OperatorToken @operator)
        : base(@operator)
        {
        }

        public BooleanAttribute Evaluate(AbstractAttribute left, AbstractAttribute right)
        {
            if (string.Compare(Operator, "is") == 0)
            {
                if (left is IEquality equality)
                    return equality.Is(right);
                else
                    return new(false);
            }
            if (string.Compare(Operator, "isnot") == 0)
            {
                if (left is IEquality equality)
                    return equality.IsNot(right);
                else
                    return new(true);
            }
            if (string.Compare(Operator, "islessthan") == 0)
            {
                if (left is IOrder order)
                    return order.IsLessThan(right);
                else
                    return new(false);
            }
            if (string.Compare(Operator, "ismorethan") == 0)
            {
                if (left is IOrder order)
                    return order.IsMoreThan(right);
                else
                    return new(false);
            }
            if (string.Compare(Operator, "isatleast") == 0)
            {
                if (left is IOrder order)
                    return order.IsAtLeast(right);
                else
                    return new(false);
            }
            if (string.Compare(Operator, "isatmost") == 0)
            {
                if (left is IOrder order)
                    return order.IsAtMost(right);
                else
                    return new(false);
            }

            throw new Exception($"Erro");
        }

        public override AbstractResult? ConvertToResult()
            => null;
    }
}
