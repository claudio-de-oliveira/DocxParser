namespace ConcreteLL.Expressions
{
    public class OrExp : AbsExpression
    {
        public AbsExpression LeftExp { get; set; }
        public AbsExpression RightExp { get; set; }

        public OrExp(AbsExpression leftExp, AbsExpression rightExp)
        {
            LeftExp = leftExp;
            RightExp = rightExp;
        }

        public override object Evaluate(Dictionary<string, Data.Variable> variables)
            => (bool)LeftExp.Evaluate(variables) || (bool)RightExp.Evaluate(variables);

        public override string ToString()
            => $"{LeftExp} or {RightExp}";

        public override string ToXml()
            => $"<or>{LeftExp.ToXml()}{RightExp.ToXml()}</or>";
    }
}
