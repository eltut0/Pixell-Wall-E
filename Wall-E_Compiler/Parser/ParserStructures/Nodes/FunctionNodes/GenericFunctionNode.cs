using System.Collections.Generic;

namespace Parser
{
    abstract class GenericFunction(string lex, int line, FunctionType functionType, List<GenericNode> arguments) : GenericNode(lex, line)
    {
        public FunctionType FunctionType { get; private set; } = functionType;
        public List<GenericNode> Arguments { get; private set; } = arguments;
        public abstract void ValidateArgument();
    }
    public enum FunctionType
    {
        Spawn,
        Color,
        Size,
        DrawLine,
        DrawCircle,
        DrawRectangle,
        Fill,
        GetActualX,
        GetActualY,
        GetCanvasSize,
        GetColorCount,
        IsBrushColor,
        IsCanvasColor,
        IsBrushSize,
    }
}