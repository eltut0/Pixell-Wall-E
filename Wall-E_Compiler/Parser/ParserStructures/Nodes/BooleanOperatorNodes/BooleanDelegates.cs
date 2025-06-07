namespace Parser
{
    abstract partial class GenericBooleanNode : GenericNode
    {
        public delegate bool BooleanOperation(bool x, bool y);
        public delegate bool BooleanComparison(int x, int y);

        //logical operations
        public static bool And(bool a, bool b) => a && b;
        public static bool Or(bool a, bool b) => a || b;
        //comparison operations
        public static bool LessThan(int a, int b) => a < b;
        public static bool GreaterThan(int a, int b) => a > b;
        public static bool LessOrEqual(int a, int b) => a <= b;
        public static bool GreaterOrEqual(int a, int b) => a >= b;
        public static bool Equal(int a, int b) => a == b;
    }
}