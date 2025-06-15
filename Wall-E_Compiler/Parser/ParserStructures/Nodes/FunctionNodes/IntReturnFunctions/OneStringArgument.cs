using Godot;
using System.Collections.Generic;
using ParserLibrary;

namespace Parser
{
    class OneStringArgumentReturn(string lex, int line, FunctionType functionType, List<GenericNode> arguments) : GenericFunction(lex, line, functionType, arguments)
    {
        protected delegate int Operation(string x);
        private readonly Operation _Operation = _operations[functionType];
        private static readonly Dictionary<FunctionType, Operation> _operations = new()
        {
            {FunctionType.IsBrushColor, IsBrushColor},
        };
        protected override void SpecialValidation()
        {
            foreach (var arg in Children)
            {
                if (!arg.IsString)
                {
                    _ = new Exception(ExceptionType.Argument, Line, $"Non valid argument");
                    return;
                }
            }
        }
        public override void ExecuteNode()
        {
            Children[0].ExecuteNode();
            Result = _Operation(Children[0].Lex);
        }

        static int IsBrushColor(string x)
        {
            if (Library.ColorsDic.TryGetValue(x, out Color color))
            {
                if (color == Compiler.CodeCompiler.BrushColor)
                {
                    return 1;
                }
            }

            return 0;
        }
    }
}