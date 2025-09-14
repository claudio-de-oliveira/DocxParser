namespace ConcreteLL.Expressions
{
    public class NotExp : AbsExpression
    {
        public AbsExpression Exp { get; set; }

        public NotExp(AbsExpression exp)
        {
            Exp = exp;
        }
        public override object Evaluate(Dictionary<string, Data.Variable> variables)
            => !(bool)Exp.Evaluate(variables);

        public override string ToString()
            => $"not {Exp}";

        public override string ToXml()
            => $"<not>{Exp.ToXml()}</not>";
    }
}
