using AbstractLL;

using ConcreteLL.Interfaces;
using ConcreteLL.Results;

namespace ConcreteLL.Attributes
{
    internal class DecimalAttribute : AbstractAttribute, IOrder, IAdditive, IMultiplicative
    {
        public double Value { get; init; }

        public DecimalAttribute(double value)
        {
            Value = value;
        }

        public BooleanAttribute IsLessThan(AbstractAttribute other)
        {
            if (other is IntegerAttribute integer)
                return new BooleanAttribute(Value < integer.Value);
            if (other is DecimalAttribute @decimal)
                return new BooleanAttribute(Value < @decimal.Value);

            return new BooleanAttribute(false);
        }

        public BooleanAttribute IsMoreThan(AbstractAttribute other)
        {
            if (other is IntegerAttribute integer)
                return new BooleanAttribute(Value > integer.Value);
            if (other is DecimalAttribute @decimal)
                return new BooleanAttribute(Value > @decimal.Value);

            return new BooleanAttribute(false);
        }

        public BooleanAttribute IsAtLeast(AbstractAttribute other)
        {
            if (other is IntegerAttribute integer)
                return new BooleanAttribute(Value >= integer.Value);
            if (other is DecimalAttribute @decimal)
                return new BooleanAttribute(Value >= @decimal.Value);

            return new BooleanAttribute(false);
        }

        public BooleanAttribute IsAtMost(AbstractAttribute other)
        {
            if (other is IntegerAttribute integer)
                return new BooleanAttribute(Value <= integer.Value);
            if (other is DecimalAttribute @decimal)
                return new BooleanAttribute(Value <= @decimal.Value);

            return new BooleanAttribute(false);
        }

        public BooleanAttribute Is(AbstractAttribute other)
        {
            if (other is IntegerAttribute integer)
                return new BooleanAttribute(Value == integer.Value);
            if (other is DecimalAttribute @decimal)
                return new BooleanAttribute(Value == @decimal.Value);

            return new BooleanAttribute(false);
        }

        public BooleanAttribute IsNot(AbstractAttribute other)
        {
            if (other is IntegerAttribute integer)
                return new BooleanAttribute(Value != integer.Value);
            if (other is DecimalAttribute @decimal)
                return new BooleanAttribute(Value != @decimal.Value);

            return new BooleanAttribute(false);
        }

        public AbstractAttribute Add(AbstractAttribute other)
        {
            if (other is IntegerAttribute integer)
                return new DecimalAttribute(Value + integer.Value);
            if (other is DecimalAttribute @decimal)
                return new DecimalAttribute(Value + @decimal.Value);

            throw new Exception($"Erro");
        }

        public AbstractAttribute Subtract(AbstractAttribute other)
        {
            if (other is IntegerAttribute integer)
                return new DecimalAttribute(Value - integer.Value);
            if (other is DecimalAttribute @decimal)
                return new DecimalAttribute(Value - @decimal.Value);

            throw new Exception($"Erro");
        }

        public AbstractAttribute Multiply(AbstractAttribute other)
        {
            if (other is IntegerAttribute integer)
                return new DecimalAttribute(Value * integer.Value);
            if (other is DecimalAttribute @decimal)
                return new DecimalAttribute(Value * @decimal.Value);

            throw new Exception($"Erro");
        }

        public AbstractAttribute Divide(AbstractAttribute other)
        {
            if (other is IntegerAttribute integer)
                return new DecimalAttribute(Value / integer.Value);
            if (other is DecimalAttribute @decimal)
                return new DecimalAttribute(Value / @decimal.Value);

            throw new Exception($"Erro");
        }

        public AbstractAttribute IntegerDivide(AbstractAttribute other)
        {
            throw new Exception($"Erro");
        }

        public override AbstractResult? ConvertToResult()
            => new DecimalResult(Value);
    }
}
