namespace ConcreteLL.Expressions
{
    public abstract class AbsExpression
    {
        public abstract object Evaluate(Dictionary<string, Data.Variable> variables);
        public virtual object Evaluate(Dictionary<string, Data.Variable> variables, Dictionary<string, object> dictionary)
        {
            var key = ToString()!;
            if (dictionary.ContainsKey(key))
                return dictionary[key];
            var result = Evaluate(variables);
            dictionary.Add(key, result);
            return result;
        }
        public abstract string ToXml();
    }
}
