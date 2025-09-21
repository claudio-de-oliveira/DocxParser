namespace ConcreteLL.Expressions
{
    public abstract class AbsExpression
    {
        public abstract object Evaluate(Dictionary<string, Data.Variable> variables);
        public virtual object Evaluate(Dictionary<string, Data.Variable> variables, Dictionary<string, object> dictionary)
        {
            var key = ToString()!;
            if (dictionary.TryGetValue(key, out object? value))
                return value;
            var result = Evaluate(variables);
            dictionary.Add(key, result);
            return result;
        }
        public abstract string ToXml();
    }
}
