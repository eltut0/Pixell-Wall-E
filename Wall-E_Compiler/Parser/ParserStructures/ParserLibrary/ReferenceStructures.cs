namespace ParserLibrary
{
    public static partial class Library
    {
        private static readonly string[] Functions = [
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

        private static readonly string[] BooleanOperators = [
            "||", "&&"
        ];
        private static readonly string[] ArithmeticOperators = [
            "+", "-", "*", "/", "%", "**"
        ];

        private static readonly string[] VoidFunctions = [
            "Spawn",
            "Color",
            "Size",
            "DrawLine",
            "DrawCircle",
            "DrawRectangle",
            "Fill",
        ];

        private static readonly string[] ReturnFunctions = [
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