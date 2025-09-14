using AbstractLL;

using ConcreteLL.Attributes;

namespace ConcreteLL.Interfaces
{
    internal interface ILogic
    {
        BooleanAttribute And(AbstractAttribute other);
        BooleanAttribute Or(AbstractAttribute other);
        BooleanAttribute Xor(AbstractAttribute other);
    }
}
