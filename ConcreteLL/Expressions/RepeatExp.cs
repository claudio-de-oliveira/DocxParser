namespace ConcreteLL.Expressions
{
    public class RepeatExp : AbsExpression
    {
        public AbsExpression Exp { get; set; }
        public int Index { get; set; } = 0;

        public RepeatExp(AbsExpression exp)
        {
            Exp = exp;
        }

        public override object Evaluate(Dictionary<string, ConcreteLL.Data.Variable> variables)
            => Exp.Evaluate(variables);

        public override string ToString()
            => $"repeat {Exp}";

        public override string ToXml()
            => $"<repeat>{Exp.ToXml()}</repeat>";
    }

    public class UnrepeatedExp : AbsExpression
    {
        public AbsExpression Exp { get; set; }

        public UnrepeatedExp(AbsExpression exp)
        {
            Exp = exp;
        }

        public override object Evaluate(Dictionary<string, ConcreteLL.Data.Variable> variables)
            => Exp.Evaluate(variables);

        public override object Evaluate(Dictionary<string, Data.Variable> variables, Dictionary<string, object> dictionary)
        {
            if (dictionary.ContainsKey(ToString()))
                return "";
            return base.Evaluate(variables, dictionary);
        }

        public override string ToString()
            => $"unrepeated({Exp})";

        public override string ToXml()
            => $"<unrepeated>{Exp.ToXml()}</unrepeated>";
    }
}

