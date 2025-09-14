using ConcreteLL.Data;

namespace ConcreteLL.Expressions
{
    public class VariableExp : AbsExpression
    {
        public Data.Variable Variable { get; set; }
        public bool HasBeenEvaluated { get; set; } = false;

        public VariableExp(Data.Variable variable)
        {
            Variable = variable;
        }

        public string Type => Variable.DataType!;
        public string Name => Variable.Name!;

        public override object Evaluate(Dictionary<string, ConcreteLL.Data.Variable> variables)
        {
            var variable = variables[Variable.Name!];

            HasBeenEvaluated = true;

            if (string.Compare(Type, "String", true) == 0)
            {
                if (variable.Value is string str)
                    return str;
                else if (variable.Value is object[] arr)
                    return arr;
            }
            else if (string.Compare(Type, "Date", true) == 0)
            {
                if (variable.Value is string str)
                    return DateTime.Parse(str);
                else if (variable.Value is object[] arr)
                    return arr.ToList().Select((x) => DateTime.Parse((string)x));
            }
            else if (string.Compare(Type, "Time", true) == 0)
            {
                if (variable.Value is string str)
                    return DateTime.Parse(str);
                else if (variable.Value is object[] arr)
                    return arr.ToList().Select((x) => DateTime.Parse((string)x));
            }
            else if (string.Compare(Type, "boolean", true) == 0)
            {
                if (variable.Value is bool b)
                    return b;
                else if (variable.Value is object[] arr)
                    return arr.ToList().Select((x) => bool.Parse((string)x)).ToArray();
            }
            else if (string.Compare(Type, "integer", true) == 0)
            {
                if (variable.Value is long b)
                    return b;
                else if (variable.Value is object[] arr)
                    return arr.ToList().Select((x) => (long)x).ToArray();
            }
            else if (string.Compare(Type, "decimal", true) == 0)
            {
                if (variable.Value is double b)
                    return b;
                else if (variable.Value is object[] arr)
                    return arr.ToList().Select((x) => (double)x).ToArray();
            }

            throw new Exception();
        }

        public override string ToString()
            => $"{Variable.Name}";

        public override string ToXml()
            => $"<variable name=\"{Variable.Name}\"/>";
    }
}
