using AbstractLL;

namespace ConcreteLL.Attributes
{
    internal class FunctionAttribute : AbstractAttribute
    {
        public string Name { get; init; }

        public FunctionAttribute(string name)
        {
            Name = name;
        }

        public override AbstractResult? ConvertToResult()
        {
            throw new NotImplementedException();
        }
    }
}
