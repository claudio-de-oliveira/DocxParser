namespace ConcreteLL.Expressions
{
    public class FormatExp : AbsExpression
    {
        public AbsExpression Exp { get; set; }
        public LiteralExp Format { get; set; }

        public FormatExp(AbsExpression exp, LiteralExp format)
        {
            Exp = exp;
            Format = format;
        }

        public override object Evaluate(Dictionary<string, ConcreteLL.Data.Variable> variables)
        {
            var exp = Exp.Evaluate(variables);
            var format = Format.Evaluate(variables);

            if (exp is DateTime dt)
                return dt.ToString((string)format);

            return exp.ToString()!;
        }

        public override string ToXml()
            => $"<format mask=\"{Format.ToXml()}\">{Exp.ToXml()}</format>";
    }
}
