using System;

namespace Parser
{
    partial class ArithmeticOperatorNode : GenericNode
    {
        public delegate int ArithmeticOperator(int x, int y);

        public static int Sum(int x, int y) => x + y;
        public static int Subtract(int x, int y) => x - y;
        public static int Multiply(int x, int y) => x * y;
        public static int Divide(int x, int y)
        {
            if (y != 0)
            { return x / y; }
            _ = new Exception(ExceptionType.DividedByZero, -1, $"{x}/{y}");
            return 0;
        }
        public static int Pow(int x, int y) => Convert.ToInt32(Math.Pow(x, y));
        public static int Modulo(int x, int y) => x % y;

    }
}