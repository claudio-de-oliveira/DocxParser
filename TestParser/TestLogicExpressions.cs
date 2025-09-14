using ConcreteLL;
using ConcreteLL.Expressions;

namespace TestParser
{
    public class TestLogicExpressions
    {
        private static Dictionary<string, ConcreteLL.Data.Variable> variables = Variables.GetVariables();

        [Fact]
        public void TestAndWithSuccess()
        {
            Parser parser1 = new(variables);
            Parser.Dump = true;
            var exp1 = parser1.Parse("PassengerNumber is 2 and SingleReturn is \"Return\"", null);
            Assert.NotNull(exp1);
            Assert.True(exp1 is AndExp);
            var result1 = ((AndExp)exp1).Evaluate(variables);
            Assert.True((bool)result1);
        }

        [Fact]
        public void TestOrWithSuccess()
        {
            Parser parser1 = new(variables);
            Parser.Dump = true;
            var exp1 = parser1.Parse("PassengerNumber isnot 2 or SingleReturn is \"Return\"", null);
            Assert.NotNull(exp1);
            Assert.True(exp1 is OrExp);
            var result1 = ((OrExp)exp1).Evaluate(variables);
            Assert.True((bool)result1);
        }

        [Fact]
        public void TestOrWithoutSuccess()
        {
            Parser parser1 = new(variables);
            Parser.Dump = true;
            var exp1 = parser1.Parse("PassengerNumber isnot 2 or SingleReturn isnot \"Return\"", null);
            Assert.NotNull(exp1);
            Assert.True(exp1 is OrExp);
            var result1 = ((OrExp)exp1).Evaluate(variables);
            Assert.False((bool)result1);
        }

        [Fact]
        public void TestNotWithSuccess()
        {
            Parser parser1 = new(variables);
            Parser.Dump = true;
            var exp1 = parser1.Parse("not (PassengerNumber isnot 2 or SingleReturn isnot \"Return\")", null);
            Assert.NotNull(exp1);
            Assert.True(exp1 is NotExp);
            var result1 = ((NotExp)exp1).Evaluate(variables);
            Assert.True((bool)result1);
        }
    }
}
