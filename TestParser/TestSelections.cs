namespace TestParser
{
    public class TestSelections
    {
        private static Dictionary<string, ConcreteLL.Data.Variable> variables = Variables.GetVariables();

        // [Fact]
        // public void TestSelection1()
        // {
        //     Parser parser = new(variables);
        //     var exp1 = parser.Parse("Select PassengerNumber", null);
        //     Assert.NotNull(exp1);
        //     Assert.True(exp1 is FunctionExp);
        //     var result1 = ((FunctionExp)exp1).Evaluate(variables);
        // }
    }
}
