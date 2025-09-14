namespace ConcreteLL.Expressions
{
    public class FalseExp : AbsExpression
    {

        public override object Evaluate(Dictionary<string, Data.Variable> variables)
            => false;

        public override string ToString()
            => "false";

        public override string ToXml()
            => $"<false/>";
    }
}
