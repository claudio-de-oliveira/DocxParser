namespace ConcreteLL.Expressions
{
    public class RelExp : AbsExpression
    {
        public AbsExpression LeftExp { get; set; }
        public AbsExpression RightExp { get; set; }
        public string Relation { get; set; }

        public RelExp(string relation, AbsExpression leftExp, AbsExpression rightExp)
        {
            Relation = relation;
            LeftExp = leftExp;
            RightExp = rightExp;
        }

        public override object Evaluate(Dictionary<string, Data.Variable> variables)
        {
            if (string.Compare(Relation, "is") == 0)
            {
                var leftRes = LeftExp.Evaluate(variables);
                var rightRes = RightExp.Evaluate(variables);

                return leftRes.Equals(rightRes);
            }
            if (string.Compare(Relation, "isnot") == 0)
            {
                var leftRes = LeftExp.Evaluate(variables);
                var rightRes = RightExp.Evaluate(variables);

                return !leftRes.Equals(rightRes);
            }
            if (string.Compare(Relation, "islessthan") == 0)
            {
                var leftRes = LeftExp.Evaluate(variables);
                var rightRes = RightExp.Evaluate(variables);

                (leftRes, rightRes) = AssegureTypeCompatility(leftRes, rightRes);

                if (leftRes is string a && rightRes is string b)
                    return string.Compare(a, b) < 0;
                if (leftRes is long left1 && rightRes is long right1)
                    return left1 < right1;
                if (leftRes is double left2 && rightRes is double right2)
                    return left2 < right2;
                if (leftRes is long && rightRes is double || leftRes is double && rightRes is long)
                    return Convert.ToDouble(leftRes) < Convert.ToDouble(rightRes);

                return false;
            }
            if (string.Compare(Relation, "ismorethan") == 0)
            {
                var leftRes = LeftExp.Evaluate(variables);
                var rightRes = RightExp.Evaluate(variables);

                (leftRes, rightRes) = AssegureTypeCompatility(leftRes, rightRes);

                if (leftRes is string a && rightRes is string b)
                    return string.Compare(a, b) > 0;
                if (leftRes is long left1 && rightRes is long right1)
                    return left1 > right1;
                if (leftRes is double left2 && rightRes is double right2)
                    return left2 > right2;
                if (leftRes is long && rightRes is double || leftRes is double && rightRes is long)
                    return Convert.ToDouble(leftRes) > Convert.ToDouble(rightRes);

                return false;
            }
            if (string.Compare(Relation, "isatleast") == 0)
            {
                var leftRes = LeftExp.Evaluate(variables);
                var rightRes = RightExp.Evaluate(variables);

                (leftRes, rightRes) = AssegureTypeCompatility(leftRes, rightRes);

                if (leftRes is string a && rightRes is string b)
                    return string.Compare(a, b) >= 0;
                if (leftRes is long left1 && rightRes is long right1)
                    return left1 >= right1;
                if (leftRes is double left2 && rightRes is double right2)
                    return left2 >= right2;
                if (leftRes is long && rightRes is double || leftRes is double && rightRes is long)
                    return Convert.ToDouble(leftRes) >= Convert.ToDouble(rightRes);

                return false;
            }
            if (string.Compare(Relation, "isatmost") == 0)
            {
                var leftRes = LeftExp.Evaluate(variables);
                var rightRes = RightExp.Evaluate(variables);

                (leftRes, rightRes) = AssegureTypeCompatility(leftRes, rightRes);

                if (leftRes is string a && rightRes is string b)
                    return string.Compare(a, b) <= 0;
                if (leftRes is long left1 && rightRes is long right1)
                    return left1 <= right1;
                if (leftRes is double left2 && rightRes is double right2)
                    return left2 <= right2;
                if (leftRes is long && rightRes is double || leftRes is double && rightRes is long)
                    return Convert.ToDouble(leftRes) <= Convert.ToDouble(rightRes);

                return false;
            }

            throw new Exception($"Erro");
        }


        private Tuple<object, object> AssegureTypeCompatility(object a, object b)
        {
            if (a.GetType() == b.GetType())
                return new(a, b);
            if (a is double)
                return new(a, Convert.ToDouble(b));
            if (b is double)
                return new(Convert.ToDouble(a), b);

            throw new NotImplementedException();
        }

        public override string ToString()
            => $"{LeftExp} {Relation} {RightExp}";

        public override string ToXml()
            => $"<rel name=\"{Relation}\">{LeftExp.ToXml()}{RightExp.ToXml()}</rel>";
    }
}
