using ConcreteLL;
using ConcreteLL.Expressions;

namespace TestParser
{
    public class TestRepeat
    {
        private static Dictionary<string, ConcreteLL.Data.Variable> variables = Variables.GetVariables();


        [Fact]
        public void TestUnrepeatedWithSuccess()
        {
            Parser parser1 = new(variables);
            var exp1 = parser1.Parse("UnRepeated(AddPassYN)", null);
            Assert.NotNull(exp1);
            Assert.True(exp1 is UnrepeatedExp);
            var result1 = ((UnrepeatedExp)exp1).Evaluate(variables);

            Parser parser2 = new(variables);
            var exp2 = parser2.Parse("AddPassYN", null);
            Assert.NotNull(exp2);
            Assert.True(exp2 is VariableExp);
            var result2 = ((VariableExp)exp2).Evaluate(variables);

            Assert.Equal(result1, result2);
        }

        [Fact]
        public void TestRepeatWithSuccess()
        {
            Parser parser1 = new(variables);
            var exp1 = parser1.Parse("Repeat(PassengerNumber)", null);
            Assert.NotNull(exp1);
            Assert.True(exp1 is RepeatExp);
            var result1 = ((RepeatExp)exp1).Evaluate(variables);

            Parser parser2 = new(variables);
            var exp2 = parser2.Parse("PassengerNumber", null);
            Assert.NotNull(exp2);
            Assert.True(exp2 is VariableExp);
            var result2 = ((VariableExp)exp2).Evaluate(variables);

            Assert.Equal(result1, result2);
        }

        [Fact]
        public void TestALternativeRepeatWithSuccess()
        {
            Parser parser1 = new(variables);
            var exp1 = parser1.Parse("Repeat PassengerNumber", null);
            Assert.NotNull(exp1);
            Assert.True(exp1 is RepeatExp);
            var result1 = ((RepeatExp)exp1).Evaluate(variables);

            Parser parser2 = new(variables);
            var exp2 = parser2.Parse("PassengerNumber", null);
            Assert.NotNull(exp2);
            Assert.True(exp2 is VariableExp);
            var result2 = ((VariableExp)exp2).Evaluate(variables);

            Assert.Equal(result1, result2);
        }
    }
}
