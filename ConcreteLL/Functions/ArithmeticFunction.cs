namespace ConcreteLL.Operations
{
    public readonly struct ArithmeticFunction
    {
        public static double Abs(double n) => Math.Abs(n);
        public static double Larger(double m, double n) => m > n ? m : n;
        public static int Mod(int a, int b) => a % b;
        public static double Power(double m, double n) => Math.Pow(m, n);
        public static int Sign(double n) => n == 0 ? 0 : n < 0 ? -1 : 1;
        public static double Smaller(double m, double n) => m < n ? m : n;
        public static double Sqrt(double n) => Math.Sqrt(n);
        public static int Round(double n) => (int)(n + .5);
        public static int RoundDown(double n) => (int)n;
        public static int RoundUp(double n) => n > (int)n ? ((int)n + 1) : (int)n;
        public static int Truncate(double n) => (int)n;
    }
}