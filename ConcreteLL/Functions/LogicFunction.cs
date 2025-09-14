namespace ConcreteLL.Operations
{
    public readonly struct LogicFunction
    {
        public static bool Is(string a, string b) => string.Compare(a, b) == 0;
        public static bool IsLessThan(string a, string b) => string.Compare(a, b) < 0;
        public static bool IsMoreThan(string a, string b) => string.Compare(a, b) > 0;
        public static bool IsNot(string a, string b) => string.Compare(a, b) != 0;
        public static bool IsAtLeast(string a, string b) => string.Compare(a, b) >= 0;
        public static bool IsAtMost(string a, string b) => string.Compare(a, b) <= 0;

        public static bool Is(double a, double b) => a == b;
        public static bool IsLessThan(double a, double b) => a < b;
        public static bool IsMoreThan(double a, double b) => a > b;
        public static bool IsNot(double a, double b) => a != b;
        public static bool IsAtLeast(double a, double b) => a >= b;
        public static bool IsAtMost(double a, double b) => a <= b;

        public static bool Is(bool a, bool b) => a == b;
        public static bool IsNot(bool a, bool b) => a != b;
    }
}