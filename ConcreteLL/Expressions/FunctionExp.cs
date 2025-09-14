using ConcreteLL.Functions;
using ConcreteLL.Operations;

namespace ConcreteLL.Expressions
{
    public class FunctionExp : AbsExpression
    {
        public string Name { get; init; }
        public List<AbsExpression> Parameters { get; init; }

        public FunctionExp(string name, List<AbsExpression> parameters)
        {
            Name = name;
            Parameters = parameters;
        }

        public override object Evaluate(Dictionary<string, ConcreteLL.Data.Variable> variables)
        {
            if (string.Compare(Name, "Abs", true) == 0)
            {
                return ArithmeticFunction.Abs(
                    Convert.ToDouble(Parameters[0].Evaluate(variables))
                    );
            }
            else if (string.Compare(Name, "Larger", true) == 0)
            {
                return ArithmeticFunction.Larger(
                    Convert.ToDouble(Parameters[0].Evaluate(variables)),
                    Convert.ToDouble(Parameters[1].Evaluate(variables))
                    );
            }
            else if (string.Compare(Name, "Mod", true) == 0)
            {
                return ArithmeticFunction.Mod(
                    Convert.ToInt32(Parameters[0].Evaluate(variables)),
                    Convert.ToInt32(Parameters[1].Evaluate(variables))
                    );
            }
            else if (string.Compare(Name, "Power", true) == 0)
            {
                return ArithmeticFunction.Power(
                    Convert.ToDouble(Parameters[0].Evaluate(variables)),
                    Convert.ToDouble(Parameters[1].Evaluate(variables))
                    );
            }
            else if (string.Compare(Name, "Sign", true) == 0)
            {
                return ArithmeticFunction.Sign(
                    Convert.ToDouble(Parameters[0].Evaluate(variables))
                    );
            }
            else if (string.Compare(Name, "Smaller", true) == 0)
            {
                return ArithmeticFunction.Smaller(
                    Convert.ToDouble(Parameters[0].Evaluate(variables)),
                    Convert.ToDouble(Parameters[1].Evaluate(variables))
                    );
            }
            else if (string.Compare(Name, "Sqrt", true) == 0)
            {
                return ArithmeticFunction.Sqrt(
                    Convert.ToDouble(Parameters[0].Evaluate(variables))
                    );
            }
            else if (string.Compare(Name, "Round", true) == 0)
            {
                return ArithmeticFunction.Round(
                    Convert.ToDouble(Parameters[0].Evaluate(variables))
                    );
            }
            else if (string.Compare(Name, "RoundDown", true) == 0)
            {
                return ArithmeticFunction.RoundDown(
                    Convert.ToDouble(Parameters[0].Evaluate(variables))
                    );
            }
            else if (string.Compare(Name, "RoundUp", true) == 0)
            {
                return ArithmeticFunction.RoundUp(
                    Convert.ToDouble(Parameters[0].Evaluate(variables))
                    );
            }
            else if (string.Compare(Name, "Truncate", true) == 0)
            {
                return ArithmeticFunction.Truncate(
                    Convert.ToDouble(Parameters[0].Evaluate(variables))
                    );
            }
            else if (string.Compare(Name, "Date", true) == 0)
            {
                return DateFunction.Date(
                    Convert.ToInt32(Parameters[0].Evaluate(variables)),
                    Convert.ToInt32(Parameters[1].Evaluate(variables)),
                    Convert.ToInt32(Parameters[2].Evaluate(variables))
                    );
            }
            else if (string.Compare(Name, "ISODate", true) == 0)
            {
                return DateFunction.ISODate(
                    Parameters[0].Evaluate(variables).ToString()!
                    );
            }
            else if (string.Compare(Name, "Today", true) == 0)
            {
                return DateFunction.Today();
            }
            else if (string.Compare(Name, "Day", true) == 0)
            {
                return DateFunction.Day(
                    (DateTime)Parameters[0].Evaluate(variables)
                    );
            }
            else if (string.Compare(Name, "Days360", true) == 0)
            {
                return DateFunction.Days360(
                    (DateTime)Parameters[0].Evaluate(variables),
                    (DateTime)Parameters[1].Evaluate(variables)
                    );
            }
            else if (string.Compare(Name, "DaysAfter", true) == 0)
            {
                return DateFunction.DaysAfter(
                    (DateTime)Parameters[0].Evaluate(variables),
                    Convert.ToInt32(Parameters[1].Evaluate(variables))
                    );
            }
            else if (string.Compare(Name, "DaysBefore", true) == 0)
            {
                return DateFunction.DaysBefore(
                    (DateTime)Parameters[0].Evaluate(variables),
                    Convert.ToInt32(Parameters[1].Evaluate(variables))
                    );
            }
            else if (string.Compare(Name, "DaysBetween", true) == 0)
            {
                return DateFunction.DaysBetween(
                    (DateTime)Parameters[0].Evaluate(variables),
                    (DateTime)Parameters[1].Evaluate(variables)
                    );
            }
            else if (string.Compare(Name, "Weekday", true) == 0)
            {
                return DateFunction.Weekday(
                    (DateTime)Parameters[0].Evaluate(variables)
                    );
            }
            else if (string.Compare(Name, "EndOfMonth", true) == 0)
            {
                return DateFunction.EndOfMonth(
                    (DateTime)Parameters[0].Evaluate(variables)
                    );
            }
            else if (string.Compare(Name, "Month", true) == 0)
            {
                return DateFunction.Month(
                    (DateTime)Parameters[0].Evaluate(variables)
                    );
            }
            else if (string.Compare(Name, "Year") == 0)
            {
                return DateFunction.Year(
                    (DateTime)Parameters[0].Evaluate(variables)
                    );
            }
            else if (string.Compare(Name, "NearestWeekdayAfter", true) == 0)
            {
                throw new NotImplementedException();
            }
            else if (string.Compare(Name, "NearestWeekdayBefore", true) == 0)
            {
                throw new NotImplementedException();
            }
            else if (string.Compare(Name, "MonthsAfter", true) == 0)
            {
                throw new NotImplementedException();
            }
            else if (string.Compare(Name, "MonthsBefore", true) == 0)
            {
                throw new NotImplementedException();
            }
            else if (string.Compare(Name, "YearsAfter", true) == 0)
            {
                throw new NotImplementedException();
            }
            else if (string.Compare(Name, "YearsBefore", true) == 0)
            {
                throw new NotImplementedException();
            }
            else if (string.Compare(Name, "MonthsBetween", true) == 0)
            {
                throw new NotImplementedException();
            }
            else if (string.Compare(Name, "YearsBetween", true) == 0)
            {
                throw new NotImplementedException();
            }
            else if (string.Compare(Name, "Is", true) == 0)
            {
                var a = Parameters[0].Evaluate(variables);
                var b = Parameters[1].Evaluate(variables);

                (a, b) = AssegureTypeCompatility(a, b);

                if (a is string u && b is string v)
                    return LogicFunction.Is(u, v);
                if (a is bool w && b is bool x)
                    return LogicFunction.Is(w, x);
                if (a is double y && b is double z)
                    return LogicFunction.Is(y, z);
                if (a is long m && b is long n)
                    return LogicFunction.Is(m, n);

                throw new NotImplementedException();
            }
            else if (string.Compare(Name, "IsLessThan", true) == 0)
            {
                var a = Parameters[0].Evaluate(variables);
                var b = Parameters[1].Evaluate(variables);

                (a, b) = AssegureTypeCompatility(a, b);

                if (a is string u && b is string v)
                    return LogicFunction.IsLessThan(u, v);
                if (a is double x && b is double y)
                    return LogicFunction.IsLessThan(x, y);
                if (a is long m && b is long n)
                    return LogicFunction.IsLessThan(m, n);

                throw new NotImplementedException();
            }
            else if (string.Compare(Name, "IsMoreThan", true) == 0)
            {
                var a = Parameters[0].Evaluate(variables);
                var b = Parameters[1].Evaluate(variables);

                (a, b) = AssegureTypeCompatility(a, b);

                if (a is string u && b is string v)
                    return LogicFunction.IsMoreThan(u, v);
                if (a is double x && b is double y)
                    return LogicFunction.IsMoreThan(x, y);
                if (a is long m && b is long n)
                    return LogicFunction.IsMoreThan(m, n);

                throw new NotImplementedException();
            }
            else if (string.Compare(Name, "IsNot", true) == 0)
            {
                var a = Parameters[0].Evaluate(variables);
                var b = Parameters[1].Evaluate(variables);

                (a, b) = AssegureTypeCompatility(a, b);

                if (a is string u && b is string v)
                    return LogicFunction.IsNot(u, v);
                if (a is bool w && b is bool x)
                    return LogicFunction.IsNot(w, x);
                if (a is double y && b is double z)
                    return LogicFunction.IsNot(y, z);
                if (a is long m && b is long n)
                    return LogicFunction.IsNot(m, n);

                throw new NotImplementedException();
            }
            else if (string.Compare(Name, "IsAtLeast", true) == 0)
            {
                var a = Parameters[0].Evaluate(variables);
                var b = Parameters[1].Evaluate(variables);

                (a, b) = AssegureTypeCompatility(a, b);

                if (a is string v && b is string w)
                    return LogicFunction.IsAtLeast(v, w);
                if (a is double x && b is double y)
                    return LogicFunction.IsAtLeast(x, y);
                if (a is long m && b is long n)
                    return LogicFunction.IsAtLeast(m, n);

                throw new NotImplementedException();
            }
            else if (string.Compare(Name, "IsAtMost", true) == 0)
            {
                var a = Parameters[0].Evaluate(variables);
                var b = Parameters[1].Evaluate(variables);

                (a, b) = AssegureTypeCompatility(a, b);

                if (a is string v && b is string w)
                    return LogicFunction.IsAtMost(v, w);
                if (a is double x && b is double y)
                    return LogicFunction.IsAtMost(x, y);
                if (a is long m && b is long n)
                    return LogicFunction.IsAtMost(m, n);

                throw new NotImplementedException();
            }
            else if (string.Compare(Name, "Lower", true) == 0)
            {
                return StringFunction.Lower(
                    (string)Parameters[0].Evaluate(variables)
                    );
            }
            else if (string.Compare(Name, "Upper", true) == 0)
            {
                return StringFunction.Upper(
                    (string)Parameters[0].Evaluate(variables)
                    );
            }
            else
                throw new NotImplementedException();
        }

        public override string ToString()
        {
            var parameters = "";

            for (int i = 0; i < Parameters.Count - 1; i++)
                parameters += $"{Parameters[i]}, ";
            parameters += Parameters[^1];

            return $"{Name}({parameters})";
        }

        private Tuple<object, object> AssegureTypeCompatility(object a, object b)
        {
            if (a.GetType() == b.GetType())
                return new(a, b);
            if (a is double)
                return new(a, Convert.ToDouble(b));
            if (b is double)
                return new(Convert.ToDouble(a), b);

            throw new NotImplementedException();
        }

        public override string ToXml()
        {
            var parameters = "";

            for (int i = 0; i < Parameters.Count - 1; i++)
                parameters += $"{Parameters[i].ToXml()}, ";
            parameters += Parameters[^1].ToXml();

            return $"<function name=\"{Name}\">{parameters}</function>";
        }
    }
}
