using AbstractLL;

using ConcreteLL.Interfaces;
using ConcreteLL.Results;

namespace ConcreteLL.Attributes
{
    internal class BooleanAttribute : AbstractAttribute, IEquality, ILogic
    {
        public bool Value { get; init; }

        public BooleanAttribute(bool value)
        {
            Value = value;
        }

        public BooleanAttribute(BooleanAttribute attribute)
        {
            Value = attribute.Value;
        }

        public BooleanAttribute Is(AbstractAttribute other)
        {
            if (other is BooleanAttribute right)
                return new BooleanAttribute(Value == right.Value);
            return new BooleanAttribute(false);
        }
        public BooleanAttribute IsNot(AbstractAttribute other)
        {
            if (other is BooleanAttribute right)
                return new BooleanAttribute(Value != right.Value);
            return new BooleanAttribute(true);
        }

        public BooleanAttribute Not()
        {
            return new BooleanAttribute(!Value);
        }
        public BooleanAttribute And(AbstractAttribute other)
        {
            if (other is BooleanAttribute right)
                return new BooleanAttribute(Value && right.Value);

            throw new Exception();
        }
        public BooleanAttribute Or(AbstractAttribute other)
        {
            if (other is BooleanAttribute right)
                return new BooleanAttribute(Value || right.Value);

            throw new Exception();
        }
        public BooleanAttribute Xor(AbstractAttribute other)
        {
            if (other is BooleanAttribute right)
                return new BooleanAttribute(Value != right.Value);

            throw new Exception();
        }

        public override AbstractResult? ConvertToResult()
            => new BooleanResult(Value);
    }
}
