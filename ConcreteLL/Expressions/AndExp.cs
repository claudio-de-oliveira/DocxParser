namespace ConcreteLL.Expressions
{
    public class AndExp : AbsExpression
    {
        public AbsExpression LeftExp { get; set; }
        public AbsExpression RightExp { get; set; }

        public AndExp(AbsExpression leftExp, AbsExpression rightExp)
        {
            LeftExp = leftExp;
            RightExp = rightExp;
        }

        public override object Evaluate(Dictionary<string, Data.Variable> variables)
            => (bool)LeftExp.Evaluate(variables) && (bool)RightExp.Evaluate(variables);

        public override string ToString()
            => $"{LeftExp} and {RightExp}";

        public override string ToXml()
            => $"<and>{LeftExp.ToXml()}{RightExp.ToXml()}</and>";
    }
}
