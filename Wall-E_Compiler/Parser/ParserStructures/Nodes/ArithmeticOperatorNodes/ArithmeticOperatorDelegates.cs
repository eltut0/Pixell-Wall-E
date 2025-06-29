using System;

namespace Parser
{
    partial class ArithmeticOperatorNode : GenericNode
    {
        public delegate int ArithmeticOperator(int x, int y, int line);

        public static int Sum(int x, int y, int z) => x + y;
        public static int Subtract(int x, int y, int z) => x - y;
        public static int Multiply(int x, int y, int z) => x * y;
        public static int Divide(int x, int y, int z)
        {
            if (y != 0)
            { return x / y; }
            _ = new Exception(ExceptionType.DividedByZero, z + 1, $"{x}/{y}");
            return 0;
        }
        public static int Pow(int x, int y, int z) => Convert.ToInt32(Math.Pow(x, y));
        public static int Modulo(int x, int y, int z) => x % y;

    }
}