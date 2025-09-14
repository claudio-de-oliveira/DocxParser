namespace ConcreteLL.Expressions
{
    public class TrueExp : AbsExpression
    {

        public override object Evaluate(Dictionary<string, Data.Variable> variables)
            => true;

        public override string ToString()
            => "true";

        public override string ToXml()
            => $"<true/>";
    }
}
