using AbstractLL;

using ConcreteLL.Interfaces;
using ConcreteLL.Results;

namespace ConcreteLL.Attributes
{
    internal class VariableAttribute : AbstractAttribute, IOrder
    {
        public Data.Variable Value { get; init; }

        public VariableAttribute(Data.Variable value)
        {
            Value = value;
        }

        public BooleanAttribute IsLessThan(AbstractAttribute other)
        {
            if (Value.DataType is not null && Value.DataType == "String")
            {
                if (other is LiteralAttribute literal)
                    return new BooleanAttribute(string.Compare((string)Value.Value!, literal.Value) < 0);
                if (other is VariableAttribute variable)
                    return new BooleanAttribute(string.Compare((string)Value.Value!, variable.Value.Name) < 0);
            }

            return new BooleanAttribute(false);
        }

        public BooleanAttribute IsMoreThan(AbstractAttribute other)
        {
            if (Value.DataType is not null && Value.DataType == "String")
            {
                if (other is LiteralAttribute literal)
                    return new BooleanAttribute(string.Compare((string)Value.Value!, literal.Value) > 0);
                if (other is VariableAttribute variable)
                    return new BooleanAttribute(string.Compare((string)Value.Value!, variable.Value.Name) > 0);
            }

            return new BooleanAttribute(false);
        }

        public BooleanAttribute IsAtLeast(AbstractAttribute other)
        {
            if (Value.DataType is not null && Value.DataType == "String")
            {
                if (other is LiteralAttribute literal)
                    return new BooleanAttribute(string.Compare((string)Value.Value!, literal.Value) >= 0);
                if (other is VariableAttribute variable)
                    return new BooleanAttribute(string.Compare((string)Value.Value!, variable.Value.Name) >= 0);
            }

            return new BooleanAttribute(false);
        }

        public BooleanAttribute IsAtMost(AbstractAttribute other)
        {
            if (Value.DataType is not null && Value.DataType == "String")
            {
                if (other is LiteralAttribute literal)
                    return new BooleanAttribute(string.Compare((string)Value.Value!, literal.Value) <= 0);
                if (other is VariableAttribute variable)
                    return new BooleanAttribute(string.Compare((string)Value.Value!, variable.Value.Name) <= 0);
            }

            return new BooleanAttribute(false);
        }

        public BooleanAttribute Is(AbstractAttribute other)
        {
            if (Value.DataType is not null && Value.DataType == "String")
            {
                if (other is LiteralAttribute literal)
                    return new BooleanAttribute(string.Compare((string)Value.Value!, literal.Value) == 0);
                if (other is VariableAttribute variable)
                    return new BooleanAttribute(string.Compare((string)Value.Value!, variable.Value.Name) == 0);
            }

            return new BooleanAttribute(false);
        }

        public BooleanAttribute IsNot(AbstractAttribute other)
        {
            if (Value.DataType is not null && Value.DataType == "String")
            {
                if (other is LiteralAttribute literal)
                    return new BooleanAttribute(string.Compare((string)Value.Value!, literal.Value) != 0);
                if (other is VariableAttribute variable)
                    return new BooleanAttribute(string.Compare((string)Value.Value!, variable.Value.Name) != 0);
            }

            return new BooleanAttribute(false);
        }

        public override AbstractResult? ConvertToResult()
            => new VariableResult(Value/*, Value.DataType!, Value.Value!*/);
    }
}
