using AbstractLL;

namespace ConcreteLL.Interfaces
{
    internal interface IMultiplicative
    {
        AbstractAttribute Multiply(AbstractAttribute other);
        AbstractAttribute Divide(AbstractAttribute other);
        AbstractAttribute IntegerDivide(AbstractAttribute other);
    }
}
