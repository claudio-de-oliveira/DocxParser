namespace ConcreteLL
{
    internal enum PredefinedFunction
    {
        // ARITHMETIC
        Abs,                    // the non-negative or absolute value for the given number
        Larger,                 // the larger of the two numbers
        Mod,                    // the remainder after the first number is divided by the second number
        Power,                  // the first number raised to the power of the second number
                                // Currently not to be used in this version of Contract Express, reserved for future use
        Sign,                   // determines if a number is positive, negative or zero
        Smaller,                // the smaller of two numbers
        Sqrt,                   // the square root of a number
                                // Currently not to be used in this version of Contract Express, reserved for future use
        Round,                  // round a number up/down to the nearest decimal place
        RoundDown,              // round a number down to the nearest decimal place
                                // Currently not to be used in this version of Contract Express, reserved for future use
        RoundUp,                // round a number up to the nearest decimal place
                                // Currently not to be used in this version of Contract Express, reserved for future use
        Truncate,               // truncate a number to a decimal place
                                // Currently not to be used in this version of Contract Express, reserved for future use
                                // DATA TYPE 
        DataType,               // the datatype of a value

        // DATE FUNCTIONS 
        Date,                   // construct a date value from year, month and day components
        ISODate,                // convert a text value in ISO format to a date value
        Today,                  // today's date
        Day,                    // the day component of a date value
        Days360,                // the number of days between two dates assuming 360 days in a year
        DaysAfter,              // the date which is a number of days after a date
        DaysBefore,             // the date which is a number of days before a date
        DaysBetween,            // the number of days between two dates
        NearestWeekdayAfter,    // the date of the nearest weekday after a date
        NearestWeekdayBefore,   // the date of the nearest weekday before a date
        Weekday,                // the weekday number of a date
        EndOfMonth,             // the last day of the month as a number
        Month,                  // the month component of a date value
        MonthsAfter,            // the date which is a number of months after a date
        MonthsBefore,           // the date which is a number of months before a date
        MonthsBetween,          // the number of months between two dates
        Year,                   // the year component of a date value
        YearsAfter,             // the date which is a number of years after a date
        YearsBefore,            // the date which is a number of years before a date
        YearsBetween,           // the number of years between two dates
    }
}
