using AbstractLL;

namespace ConcreteLL.Interfaces
{
    internal interface IAdditive
    {
        AbstractAttribute Add(AbstractAttribute other);
        AbstractAttribute Subtract(AbstractAttribute other);
    }
}
