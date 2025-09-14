using AbstractLL;

using ConcreteLL.Tokens;

namespace ConcreteLL.Attributes
{
    internal class OperatorAttribute : AbstractAttribute
    {
        public string Operator { get; init; }

        protected OperatorAttribute(OperatorToken @operator)
        {
            Operator = @operator.Value;
        }

        public override AbstractResult? ConvertToResult()
        {
            throw new NotImplementedException();
        }
    }
}
