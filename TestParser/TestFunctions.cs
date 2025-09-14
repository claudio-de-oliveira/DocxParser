using ConcreteLL;
using ConcreteLL.Expressions;

namespace TestParser
{
    public class TestFunctions
    {
        private static Dictionary<string, ConcreteLL.Data.Variable> variables = Variables.GetVariables();

        [Fact]
        public void TestTodayWithSuccess()
        {
            Parser parser1 = new(variables);
            var exp1 = parser1.Parse("Today", null);
            Assert.NotNull(exp1);
            Assert.True(exp1 is FunctionExp);
            var result1 = ((AbsExpression)exp1).Evaluate(variables);
            Assert.True(result1 is DateTime);
        }

        [Fact]
        public void TestFormat1DateWithSuccess()
        {
            Parser parser1 = new(variables);
            var exp1 = parser1.Parse("Today Format \"yyyy\"", null);
            Assert.NotNull(exp1);
            Assert.True(exp1 is FormatExp);
            var result1 = ((AbsExpression)exp1).Evaluate(variables);
            Assert.True(result1 is string);
            Assert.True(string.Compare(DateTime.Now.Year.ToString(), (string)result1) == 0);
        }

        [Fact]
        public void TestFormat3DateWithSuccess()
        {
            Parser parser1 = new(variables);
            var exp1 = parser1.Parse("Today Format \"h:mm:ss.ff t\"", null);
            Assert.NotNull(exp1);
            Assert.True(exp1 is FormatExp);
            var result1 = ((AbsExpression)exp1).Evaluate(variables);
            Assert.True(result1 is string);
            Assert.True(string.Compare(DateTime.Now.ToString("h:mm:ss.ff t"), (string)result1) == 0);
        }

        [Fact]
        public void TestFormat4DateWithSuccess()
        {
            Parser parser1 = new(variables);
            var exp1 = parser1.Parse("Today Format \"d MMM yyyy\"", null);
            Assert.NotNull(exp1);
            Assert.True(exp1 is FormatExp);
            var result1 = ((AbsExpression)exp1).Evaluate(variables);
            Assert.True(result1 is string);
            Assert.True(string.Compare(DateTime.Now.ToString("d MMM yyyy"), (string)result1) == 0);
        }

        [Fact]
        public void TestFormat5DateWithSuccess()
        {
            Parser parser1 = new(variables);
            var exp1 = parser1.Parse("Today Format \"HH:mm:ss.f\"", null);
            Assert.NotNull(exp1);
            Assert.True(exp1 is FormatExp);
            var result1 = ((AbsExpression)exp1).Evaluate(variables);
            Assert.True(result1 is string);
            Assert.True(string.Compare(DateTime.Now.ToString("HH:mm:ss.f"), (string)result1) == 0);
        }

        [Fact]
        public void TestFormat6DateWithSuccess()
        {
            Parser parser1 = new(variables);
            var exp1 = parser1.Parse("Today Format \"dd MMM HH:mm:ss\"", null);
            Assert.NotNull(exp1);
            Assert.True(exp1 is FormatExp);
            var result1 = ((AbsExpression)exp1).Evaluate(variables);
            Assert.True(result1 is string);
            Assert.True(string.Compare(DateTime.Now.ToString("dd MMM HH:mm:ss"), (string)result1) == 0);
        }
    }
}
