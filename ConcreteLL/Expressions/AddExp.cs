namespace ConcreteLL.Expressions
{
    public class AddExp : AbsExpression
    {
        public AbsExpression LeftExp { get; set; }
        public AbsExpression RightExp { get; set; }
        public string Operator { get; set; }

        public AddExp(string op, AbsExpression leftExp, AbsExpression rightExp)
        {
            Operator = op;
            LeftExp = leftExp;
            RightExp = rightExp;
        }

        public override object Evaluate(Dictionary<string, ConcreteLL.Data.Variable> variables)
        {
            var leftResult = LeftExp.Evaluate(variables);
            var rightResult = RightExp.Evaluate(variables);

            if (string.Compare(Operator, "+") == 0)
            {
                if (leftResult is double || rightResult is double)
                    return Convert.ToDouble(leftResult) + Convert.ToDouble(rightResult);
                else
                    return Convert.ToInt64(leftResult) + Convert.ToInt64(rightResult);
            }
            if (string.Compare(Operator, "-") == 0)
            {
                if (leftResult is double || rightResult is double)
                    return Convert.ToDouble(leftResult) - Convert.ToDouble(rightResult);
                else
                    return Convert.ToInt64(leftResult) - Convert.ToInt64(rightResult);
            }

            throw new Exception();
        }

        public override string ToString()
            => $"{LeftExp} {Operator} {RightExp}";

        public override string ToXml()
            => $"<add>{LeftExp.ToXml()}{RightExp.ToXml()}</add>";
    }
}
