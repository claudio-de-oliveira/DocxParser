using AbstractLL;

using ConcreteLL.Data;
using ConcreteLL.Expressions;
using ConcreteLL.Tokens;

namespace ConcreteLL
{
    public class Environment : AbstractEnvironment<AbsExpression>
    {
        public AbsExpression? result { get; set; }

        public override AbsExpression? Result => result;

        public readonly Dictionary<string, AbstractToken> SymbolTable = new Dictionary<string, AbstractToken>();

        public readonly Dictionary<string, Variable> Variables;

        public Environment(Dictionary<string, ConcreteLL.Data.Variable> variables)
        {
            Variables = variables;

            // ARITHMETIC
            SymbolTable.Add("abs", new FunctionToken(
                Enum.GetName(typeof(PredefinedFunction), PredefinedFunction.Abs)!,
                "The non-negative or absolute value for the given number"));
            SymbolTable.Add("larger",
                new FunctionToken(Enum.GetName(typeof(PredefinedFunction), PredefinedFunction.Larger)!,
                "The larger of the two numbers"));
            SymbolTable.Add("mod",
                new FunctionToken(Enum.GetName(typeof(PredefinedFunction), PredefinedFunction.Mod)!,
                "The remainder after the first number is divided by the second number"));
            SymbolTable.Add("power",
                new FunctionToken(Enum.GetName(typeof(PredefinedFunction), PredefinedFunction.Power)!,
                "The first number raised to the power of the second number")); // Currently not to be used in this version of Contract Express, reserved for future use
            SymbolTable.Add("sign",
                new FunctionToken(Enum.GetName(typeof(PredefinedFunction), PredefinedFunction.Sign)!,
                "Determines if a number is positive, negative or zero"));
            SymbolTable.Add("smaller",
                new FunctionToken(Enum.GetName(typeof(PredefinedFunction), PredefinedFunction.Smaller)!,
                "The smaller of two numbers"));
            SymbolTable.Add("sqrt",
                new FunctionToken(Enum.GetName(typeof(PredefinedFunction), PredefinedFunction.Sqrt)!,
                "The square root of a number")); // Currently not to be used in this version of Contract Express, reserved for future use
            SymbolTable.Add("round",
                new FunctionToken(Enum.GetName(typeof(PredefinedFunction), PredefinedFunction.Round)!,
                "Round a number up/down to the nearest decimal place"));
            SymbolTable.Add("rounddown",
                new FunctionToken(Enum.GetName(typeof(PredefinedFunction), PredefinedFunction.RoundDown)!,
                "Round a number down to the nearest decimal place")); // Currently not to be used in this version of Contract Express, reserved for future use
            SymbolTable.Add("roundup",
                new FunctionToken(Enum.GetName(typeof(PredefinedFunction), PredefinedFunction.RoundUp)!,
                "Round a number up to the nearest decimal place")); // Currently not to be used in this version of Contract Express, reserved for future use
            SymbolTable.Add("truncate",
                new FunctionToken(Enum.GetName(typeof(PredefinedFunction), PredefinedFunction.Truncate)!,
                "Truncate a number to a decimal place")); // Currently not to be used in this version of Contract Express, reserved for future use

            // DATA TYPE 
            SymbolTable.Add("datatype",
                new FunctionToken(Enum.GetName(typeof(PredefinedFunction), PredefinedFunction.DataType)!,
                "The datatype of a value"));

            // DATE FUNCTIONS 
            SymbolTable.Add("date",
                new FunctionToken(Enum.GetName(typeof(PredefinedFunction), PredefinedFunction.Date)!,
                "Construct a date value from year, month and day components"));
            SymbolTable.Add("isodate",
                new FunctionToken(Enum.GetName(typeof(PredefinedFunction), PredefinedFunction.ISODate)!,
                "Convert a text value in ISO format to a date value"));
            SymbolTable.Add("today",
                new FunctionToken(Enum.GetName(typeof(PredefinedFunction), PredefinedFunction.Today)!,
                "Today's date"));
            SymbolTable.Add("day",
                new FunctionToken(Enum.GetName(typeof(PredefinedFunction), PredefinedFunction.Day)!,
                "The day component of a date value"));
            SymbolTable.Add("days360",
                new FunctionToken(Enum.GetName(typeof(PredefinedFunction), PredefinedFunction.Days360)!,
                "The number of days between two dates assuming 360 days in a year"));
            SymbolTable.Add("daysafter",
                new FunctionToken(Enum.GetName(typeof(PredefinedFunction), PredefinedFunction.DaysAfter)!,
                "The date which is a number of days after a date"));
            SymbolTable.Add("daysbefore",
                new FunctionToken(Enum.GetName(typeof(PredefinedFunction), PredefinedFunction.DaysBefore)!,
                "The date which is a number of days before a date"));
            SymbolTable.Add("daysbetween",
                new FunctionToken(Enum.GetName(typeof(PredefinedFunction), PredefinedFunction.DaysBetween)!,
                "The number of days between two dates"));
            SymbolTable.Add("nearestweekdayafter",
                new FunctionToken(Enum.GetName(typeof(PredefinedFunction), PredefinedFunction.NearestWeekdayAfter)!,
                "The date of the nearest weekday after a date"));
            SymbolTable.Add("nearestweekdaybefore",
                new FunctionToken(Enum.GetName(typeof(PredefinedFunction), PredefinedFunction.NearestWeekdayBefore)!,
                "The date of the nearest weekday before a date"));
            SymbolTable.Add("weekday",
                new FunctionToken(Enum.GetName(typeof(PredefinedFunction), PredefinedFunction.Weekday)!,
                "The weekday number of a date"));
            SymbolTable.Add("endofmonth",
                new FunctionToken(Enum.GetName(typeof(PredefinedFunction), PredefinedFunction.EndOfMonth)!,
                "The last day of the month as a number"));
            SymbolTable.Add("month",
                new FunctionToken(Enum.GetName(typeof(PredefinedFunction), PredefinedFunction.Month)!,
                "The month component of a date value"));
            SymbolTable.Add("monthsafter",
                new FunctionToken(Enum.GetName(typeof(PredefinedFunction), PredefinedFunction.MonthsAfter)!,
                "The date which is a number of months after a date"));
            SymbolTable.Add("monthsbefore",
                new FunctionToken(Enum.GetName(typeof(PredefinedFunction), PredefinedFunction.MonthsBefore)!,
                "The date which is a number of months before a date"));
            SymbolTable.Add("monthsbetween",
                new FunctionToken(Enum.GetName(typeof(PredefinedFunction), PredefinedFunction.MonthsBetween)!,
                "The number of months between two dates"));
            SymbolTable.Add("year",
                new FunctionToken(Enum.GetName(typeof(PredefinedFunction), PredefinedFunction.Year)!,
                "The year component of a date value"));
            SymbolTable.Add("yearsafter",
                new FunctionToken(Enum.GetName(typeof(PredefinedFunction), PredefinedFunction.YearsAfter)!,
                "The date which is a number of years after a date"));
            SymbolTable.Add("yearsbefore",
                new FunctionToken(Enum.GetName(typeof(PredefinedFunction), PredefinedFunction.YearsBefore)!,
                "The date which is a number of years before a date"));
            SymbolTable.Add("yearsbetween",
                new FunctionToken(Enum.GetName(typeof(PredefinedFunction), PredefinedFunction.YearsBetween)!,
                "The number of years between two dates"));
        }

        public override void Inicializa()
        {
        }
    }
}
