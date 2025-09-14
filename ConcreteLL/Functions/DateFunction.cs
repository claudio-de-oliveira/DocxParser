namespace ConcreteLL.Operations
{
    public readonly struct DateFunction
    {
        public static DateTime Date(int y, int m, int d) => new(y, m, d);
        public static DateTime ISODate(string date) => DateTime.Parse(date);
        public static DateTime Today() => DateTime.Now;
        public static int Day(DateTime date) => date.Day;
        public static int Days360(DateTime a, DateTime b) => (int)(b - a).TotalDays;
        public static DateTime DaysAfter(DateTime date, int days)
        {
            var dtOff = new DateTimeOffset(date, new TimeSpan(days, 0, 0));
            return new DateTime(dtOff.Year, dtOff.Month, dtOff.Day);
        }
        public static DateTime DaysBefore(DateTime date, int days)
        {
            var dtOff = new DateTimeOffset(date, new TimeSpan(-days, 0, 0));
            return new DateTime(dtOff.Year, dtOff.Month, dtOff.Day);
        }
        public static int DaysBetween(DateTime a, DateTime b) => (int)(b - a).TotalDays;
        public static DayOfWeek Weekday(DateTime date) => date.DayOfWeek;
        public static int EndOfMonth(DateTime date) => DateTime.DaysInMonth(date.Year, date.Month);
        public static int Month(DateTime date) => date.Month;
        public static int Year(DateTime date) => date.Year;

        public static DateTime NearestWeekdayAfter(DateTime date)
            => throw new NotImplementedException("The date of the nearest weekday after a date");
        public static DateTime NearestWeekdayBefore(DateTime date)
            => throw new NotImplementedException("The date of the nearest weekday before a date");
        public static DateTime MonthsAfter(DateTime date, int months)
            => throw new NotImplementedException("The date which is a number of months after a date");
        public static DateTime MonthsBefore(DateTime date, int months)
            => throw new NotImplementedException("The date which is a number of months before a date");
        public static DateTime YearsAfter(DateTime date, int offset)
            => throw new NotImplementedException("The date which is a number of years after a date");
        public static DateTime YearsBefore(DateTime date, int offset)
            => throw new NotImplementedException("The date which is a number of years before a date");
        public static int MonthsBetween(DateTime a, DateTime b)
            => throw new NotImplementedException("The number of months between two dates");
        public static int YearsBetween(DateTime a, DateTime b)
            => throw new NotImplementedException("The number of years between two dates");
    }
}