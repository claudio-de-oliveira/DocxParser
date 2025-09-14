using ConcreteLL;
using ConcreteLL.Expressions;

namespace TestParser
{
    public class TestRelations
    {
        private static Dictionary<string, ConcreteLL.Data.Variable> variables = Variables.GetVariables();

        [Fact]
        public void TestIsStringWithSuccess()
        {
            Parser parser1 = new(variables);
            var exp1 = parser1.Parse("SingleReturn is \"Return1\"", null);
            Assert.NotNull(exp1);
            Assert.True(exp1 is RelExp);
            var result1 = ((RelExp)exp1).Evaluate(variables);

            Parser parser2 = new(variables);
            var exp2 = parser2.Parse("SingleReturn", null);
            Assert.NotNull(exp2);
            Assert.True(exp2 is VariableExp);
            var result2 = ((VariableExp)exp2).Evaluate(variables);

            if ((bool)result1)
                Assert.True(string.Compare((string)result2, "Return") == 0);
            else
                Assert.True(string.Compare((string)result2, "Return1") != 0);
        }

        [Fact]
        public void TestIsAtLeastWithSuccess()
        {
            Parser parser1 = new(variables);
            var exp1 = parser1.Parse("PassengerNumber isatleast 2", null);
            Assert.NotNull(exp1);
            Assert.True(exp1 is RelExp);
            var result1 = ((RelExp)exp1).Evaluate(variables);
            Assert.True((bool)result1);
        }

        [Fact]
        public void TestIsAtMostWithSuccess()
        {
            Parser parser1 = new(variables);
            var exp1 = parser1.Parse("PassengerNumber isatmost 2", null);
            Assert.NotNull(exp1);
            Assert.True(exp1 is RelExp);
            var result1 = ((RelExp)exp1).Evaluate(variables);
            Assert.True((bool)result1);
        }

        [Fact]
        public void TestIsLessThanWithSuccess()
        {
            Parser parser1 = new(variables);
            var exp1 = parser1.Parse("PassengerNumber islessthan 2", null);
            Assert.NotNull(exp1);
            Assert.True(exp1 is RelExp);
            var result1 = ((RelExp)exp1).Evaluate(variables);
            Assert.False((bool)result1);
        }

        [Fact]
        public void TestIsMoreThanWithSuccess()
        {
            Parser parser1 = new(variables);
            var exp1 = parser1.Parse("PassengerNumber ismorethan 2", null);
            Assert.NotNull(exp1);
            Assert.True(exp1 is RelExp);
            var result1 = ((RelExp)exp1).Evaluate(variables);
            Assert.False((bool)result1);
        }

        [Fact]
        public void TestIsNumberWithSuccess()
        {
            Parser parser1 = new(variables);
            var exp1 = parser1.Parse("PassengerNumber is 2", null);
            Assert.NotNull(exp1);
            Assert.True(exp1 is RelExp);
            var result1 = ((RelExp)exp1).Evaluate(variables);
            Assert.True((bool)result1);
        }

        [Fact]
        public void TestIsNotWithSuccess()
        {
            Parser parser1 = new(variables);
            var exp1 = parser1.Parse("PassengerNumber isnot 2", null);
            Assert.NotNull(exp1);
            Assert.True(exp1 is RelExp);
            var result1 = ((RelExp)exp1).Evaluate(variables);
            Assert.False((bool)result1);
        }
    }
}
