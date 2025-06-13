using System.Collections.Generic;

namespace Parser
{
    abstract class GenericFunction(string lex, int line, FunctionType functionType, List<GenericNode> arguments) : GenericNode(lex, line)
    {


        public static readonly Dictionary<FunctionType, int> ArgsCount = new()
        {
            { FunctionType.Spawn,2},
            { FunctionType.Color,1},
            { FunctionType.Size,1},
            { FunctionType.DrawLine,3},
            { FunctionType.DrawCircle,3},
            { FunctionType.DrawRectangle,5},
            { FunctionType.Fill,0},
            { FunctionType.GetActualX,0},
            { FunctionType.GetActualY,0},
            { FunctionType.GetCanvasSize,0},
            { FunctionType.GetColorCount,5},
            { FunctionType.IsBrushColor,1},
            { FunctionType.IsCanvasColor,3},
            { FunctionType.IsBrushSize,1}
        };
        public FunctionType FunctionType { get; private set; } = functionType;
        public List<GenericNode> Arguments { get; private set; } = arguments;
        public void ValidateArgument()
        {
            if (!(Arguments.Count == ArgsCount[FunctionType]))
            {
                _ = new Exception(ExceptionType.Argument, Line, Lex);
                return;
            }
            SpecialValidation();
        }
        protected virtual void SpecialValidation() { }


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