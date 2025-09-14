namespace ConcreteLL.Expressions
{
    public class IfExp : AbsExpression
    {
        public AbsExpression TestExp { get; set; }
        public AbsExpression ThenExp { get; set; }
        public AbsExpression ElseExp { get; set; }

        public IfExp(AbsExpression testExp, AbsExpression thenExp, AbsExpression elseExp)
        {
            TestExp = testExp;
            ThenExp = thenExp;
            ElseExp = elseExp;
        }

        public override object Evaluate(Dictionary<string, ConcreteLL.Data.Variable> variables)
        {
            var testResult = (bool)TestExp.Evaluate(variables);
            return (testResult) ? ThenExp.Evaluate(variables) : ElseExp.Evaluate(variables);
        }

        public override string ToString()
            => $"if ({TestExp}) then {ThenExp} else {ElseExp}";

        public override string ToXml()
            => $"<if>{TestExp.ToXml()}{ThenExp.ToXml()}{ElseExp.ToXml()}</if>";
    }
}
