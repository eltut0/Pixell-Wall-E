using System;

namespace Parser
{
    partial class ArithmeticOperatorNode : GenericNode
    {
        public delegate int ArithmeticOperator(int x, int y);

        static int Sum(int x, int y) => x + y;
        static int Subtract(int x, int y) => x - y;
        static int Multiply(int x, int y) => x * y;
        static int Divide(int x, int y)
        {
            if (y != 0)
            { return x / y; }
            Exception exception = new(ExceptionType.DividedByZero, -1, $"{x}/{y}");
            return 0;
        }
        static int Pow(int x, int y) => Convert.ToInt32(Math.Pow(x, y));
        static int Modulo(int x, int y) => x % y;

    }
}