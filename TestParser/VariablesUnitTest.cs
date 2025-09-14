using ConcreteLL;
using ConcreteLL.Expressions;

namespace TestParser
{
    public class VariablesUnitTest
    {
        private static Dictionary<string, ConcreteLL.Data.Variable> variables = Variables.GetVariables();

        [Fact]
        public void TestAllVariablesWithSuccess()
        {
            foreach (var variable in variables.Values)
            {
                if (string.Compare(variable.Name, "VARIABLE_CHANGED") == 0)
                    continue;

                Parser parser = new(variables);

                var result = parser.Parse(variable.Name!, null);
                Assert.NotNull(result);
                Assert.True(result is VariableExp);
                var value = ((VariableExp)result).Evaluate(variables);
                if (variable.DataType == "String")
                {
                    if (value is string)
                        Assert.True(string.Compare((string)value, (string)variable.Value!) == 0);
                    else
                        Assert.True(value is string[]);
                }
                else if (variable.DataType == "Boolean")
                {
                    if (value is bool)
                        Assert.True((bool)value == (bool)variable.Value!);
                    else
                        Assert.True(value is bool[]);
                }
                else if (variable.DataType == "Integer")
                {
                    if (value is long)
                        Assert.True((long)value == (long)variable.Value!);
                    else
                        Assert.True(value is long[]);
                }
                else if (variable.DataType == "Date")
                {
                    Assert.True(value is DateTime || value is DateTime[]);
                }
                else if (variable.DataType == "Time")
                {
                    Assert.True(value is DateTime || value is DateTime[]);
                }
            }
        }
    }
}