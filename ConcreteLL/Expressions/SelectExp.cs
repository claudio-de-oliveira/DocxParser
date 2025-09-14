namespace ConcreteLL.Expressions
{
    public class SelectExp : AbsExpression
    {
        public List<SelectionItemExp> SelectionItems { get; set; }

        public SelectExp(List<SelectionItemExp> selectionItems)
        {
            SelectionItems = selectionItems;
        }

        public override object Evaluate(Dictionary<string, ConcreteLL.Data.Variable> variables)
        {
            throw new NotImplementedException();
        }

        public override string ToXml()
        {
            var xml = "";
            SelectionItems.ForEach(o => xml += o.ToXml());
            return $"<select>{xml}</select>";
        }
    }

    public class SelectionItemExp : AbsExpression
    {
        public VariableExp Variable { get; set; }
        public List<AbsExpression> Options { get; set; }

        public SelectionItemExp(VariableExp variable, List<AbsExpression> options)
        {
            Variable = variable;
            Options = options;
        }

        public override object Evaluate(Dictionary<string, ConcreteLL.Data.Variable> variables)
        {
            throw new NotImplementedException();
        }

        public override string ToXml()
        {
            var xml = "";
            Options.ForEach(o => xml += o.ToXml());
            return $"<item>{Variable.ToXml()}{xml}</item>";
        }
    }
}
