using ConcreteLL;
using ConcreteLL.Expressions;

namespace TestParser
{
    public class TextComplexExpressions
    {
        private static Dictionary<string, ConcreteLL.Data.Variable> variables = Variables.GetVariables();

        [Fact]
        public void TestComplexWithSuccess()
        {
            Parser parser1 = new(variables);
            Parser.Dump = true;
            var exp1 = parser1.Parse("PassengerNumber is 2 - 5*2//5+2", null);
            Assert.NotNull(exp1);
            Assert.True(exp1 is RelExp);
            var result1 = ((RelExp)exp1).Evaluate(variables);
            Assert.True((bool)result1);
        }

        [Fact]
        public void TestIfThenWithSuccess()
        {
            Parser parser = new(variables);
            var exp1 = parser.Parse("if PassengerNumber is 2 - Round(5*2/5)+2 then TravelClass else PassengerName", null);
            Assert.NotNull(exp1);
            Assert.True(exp1 is IfExp);
            var result1 = ((IfExp)exp1).Evaluate(variables);

            Parser parser2 = new(variables);
            var exp2 = parser2.Parse("TravelClass", null);
            Assert.NotNull(exp2);
            Assert.True(exp2 is VariableExp);
            var result2 = ((VariableExp)exp2).Evaluate(variables);

            Assert.True(string.Compare((string)result2, (string)result1) == 0);
        }

        [Fact]
        public void TestIfElseWithSuccess()
        {
            Parser parser = new(variables);
            var exp1 = parser.Parse("if PassengerNumber is 5*2/5+2 then TravelClass else PassengerName", null);
            Assert.NotNull(exp1);
            Assert.True(exp1 is IfExp);
            var result1 = ((IfExp)exp1).Evaluate(variables);

            Parser parser2 = new(variables);
            var exp2 = parser2.Parse("PassengerName", null);
            Assert.NotNull(exp2);
            Assert.True(exp2 is VariableExp);
            var result2 = ((VariableExp)exp2).Evaluate(variables);

            Assert.True(string.Compare((string)result2, (string)result1) == 0);
        }
    }
}
