using AbstractLL;

using ConcreteLL.Attributes;

namespace ConcreteLL.Interfaces
{
    internal interface IEquality
    {
        BooleanAttribute Is(AbstractAttribute other);
        BooleanAttribute IsNot(AbstractAttribute other);
    }
}
