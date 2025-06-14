namespace ParserLibrary
{
    public static partial class Library
    {
        public static readonly string[] Functions = [
            "Spawn",
            "Color",
            "Size",
            "DrawLine",
            "DrawCircle",
            "DrawRectangle",
            "Fill",
            "GetActualX",
            "GetActualY",
            "GetCanvasSize",
            "GetColorCount",
            "IsBrushColor",
            "IsBrushSize",
            "IsCanvasColor"
        ];

        public static readonly string[] BooleanOperators = [
            "||", "&&"
        ];
        public static readonly string[] BooleanComparators = [
            ">", "<", ">=", "<=", "=="
        ];
        public static readonly string[] ArithmeticOperators = [
            "+", "-", "*", "/", "%", "**"
        ];

        public static readonly string[] VoidFunctions = [
            "Spawn",
            "Color",
            "Size",
            "DrawLine",
            "DrawCircle",
            "DrawRectangle",
            "Fill",
        ];

        public static readonly string[] ReturnFunctions = [
            "GetActualX",
            "GetActualY",
            "GetCanvasSize",
            "GetColorCount",
            "IsBrushColor",
            "IsBrushSize",
            "IsCanvasColor"
        ];
    }
}