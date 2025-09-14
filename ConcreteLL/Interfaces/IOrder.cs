using AbstractLL;

using ConcreteLL.Attributes;

namespace ConcreteLL.Interfaces
{
    internal interface IOrder : IEquality
    {
        BooleanAttribute IsLessThan(AbstractAttribute other);
        BooleanAttribute IsMoreThan(AbstractAttribute other);
        BooleanAttribute IsAtLeast(AbstractAttribute other);
        BooleanAttribute IsAtMost(AbstractAttribute other);
    }
}
