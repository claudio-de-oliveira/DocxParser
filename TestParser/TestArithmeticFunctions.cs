using ConcreteLL;
using ConcreteLL.Expressions;

namespace TestParser
{
    public class TestArithmeticFunctions
    {
        private static Dictionary<string, ConcreteLL.Data.Variable> variables = Variables.GetVariables();

        [Fact]
        public void TestAbsFunctionWithSuccess()
        {
            Parser parser = new(variables);
            var exp1 = parser.Parse("Abs(PassengerNumber)", null);
            Assert.NotNull(exp1);
            Assert.True(exp1 is FunctionExp);
            var result1 = ((FunctionExp)exp1).Evaluate(variables);

            Parser parser2 = new(variables);
            var exp2 = parser2.Parse("PassengerNumber", null);
            Assert.NotNull(exp2);
            Assert.True(exp2 is VariableExp);
            var result2 = ((VariableExp)exp2).Evaluate(variables);

            Assert.True((long)result2 == Convert.ToInt64(result1));
        }

        [Fact]
        public void TestLargerFunctionWithSuccess()
        {
            Parser parser = new(variables);
            var exp1 = parser.Parse("Larger(4, 5)", null);
            Assert.NotNull(exp1);
            Assert.True(exp1 is FunctionExp);
            var result1 = ((FunctionExp)exp1).Evaluate(variables);

            Assert.True((double)result1 == 5);
        }

        [Fact]
        public void TestModFunctionWithSuccess()
        {
            Parser parser = new(variables);
            var exp1 = parser.Parse("Mod(7, 2)", null);
            Assert.NotNull(exp1);
            Assert.True(exp1 is FunctionExp);
            var result1 = ((FunctionExp)exp1).Evaluate(variables);

            Assert.True((int)result1 == 1);
        }

        [Fact]
        public void TestPowerFunctionWithSuccess()
        {
            Parser parser = new(variables);
            var exp1 = parser.Parse("Power(4, 2)", null);
            Assert.NotNull(exp1);
            Assert.True(exp1 is FunctionExp);
            var result1 = ((FunctionExp)exp1).Evaluate(variables);

            Assert.True((double)result1 == 16);
        }

        [Fact]
        public void TestSignFunctionWithSuccess()
        {
            Parser parser = new(variables);
            var exp1 = parser.Parse("Sign(4 - 5)", null);
            Assert.NotNull(exp1);
            Assert.True(exp1 is FunctionExp);
            var result1 = ((FunctionExp)exp1).Evaluate(variables);

            Assert.True((int)result1 == -1);
        }

        [Fact]
        public void TestSmallerFunctionWithSuccess()
        {
            Parser parser = new(variables);
            var exp1 = parser.Parse("Smaller(4, 5)", null);
            Assert.NotNull(exp1);
            Assert.True(exp1 is FunctionExp);
            var result1 = ((FunctionExp)exp1).Evaluate(variables);

            Assert.True((double)result1 == 4);
        }

        [Fact]
        public void TestSqrtFunctionWithSuccess()
        {
            Parser parser = new(variables);
            var exp1 = parser.Parse("Sqrt(16)", null);
            Assert.NotNull(exp1);
            Assert.True(exp1 is FunctionExp);
            var result1 = ((FunctionExp)exp1).Evaluate(variables);

            Assert.True((double)result1 == 4);
        }

        [Fact]
        public void TestRoundFunctionWithSuccess()
        {
            Parser parser = new(variables);
            var exp1 = parser.Parse("Round(5/2)", null);
            Assert.NotNull(exp1);
            Assert.True(exp1 is FunctionExp);
            var result1 = ((FunctionExp)exp1).Evaluate(variables);

            Assert.True((int)result1 == 3);
        }

        [Fact]
        public void TestRoundDownFunctionWithSuccess()
        {
            Parser parser = new(variables);
            var exp1 = parser.Parse("RoundDown(5/2)", null);
            Assert.NotNull(exp1);
            Assert.True(exp1 is FunctionExp);
            var result1 = ((FunctionExp)exp1).Evaluate(variables);

            Assert.True((int)result1 == 2);
        }

        [Fact]
        public void TestRoundUpFunctionWithSuccess()
        {
            Parser parser = new(variables);
            var exp1 = parser.Parse("RoundUp(5/2)", null);
            Assert.NotNull(exp1);
            Assert.True(exp1 is FunctionExp);
            var result1 = ((FunctionExp)exp1).Evaluate(variables);

            Assert.True((int)result1 == 3);
        }

        [Fact]
        public void TestTruncateFunctionWithSuccess()
        {
            Parser parser = new(variables);
            var exp1 = parser.Parse("Truncate(5/2)", null);
            Assert.NotNull(exp1);
            Assert.True(exp1 is FunctionExp);
            var result1 = ((FunctionExp)exp1).Evaluate(variables);

            Assert.True((int)result1 == 2);
        }
    }
}
