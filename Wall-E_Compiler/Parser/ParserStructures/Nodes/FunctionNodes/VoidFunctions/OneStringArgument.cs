using ParserLibrary;
using System.Collections.Generic;
using Godot;

namespace Parser
{
    class OneStringArgument(string lex, int line, FunctionType functionType, List<GenericNode> arguments) : GenericFunction(lex, line, functionType, arguments)
    {
        private delegate void Operation(string x, int line);
        private readonly Operation _operation = _operations[functionType];
        protected override void SpecialValidation()
        {
            foreach (var arg in Children)
            {
                if (!arg.IsString)
                {
                    _ = new Exception(ExceptionType.Argument, Line + 1, $"Non valid argument");
                    return;
                }
            }
        }
        public override void ExecuteNode()
        {
            Children[0].ExecuteNode();
            _operation(Children[0].Lex, Line);
        }
        private static readonly Dictionary<FunctionType, Operation> _operations = new()
        {
            {FunctionType.Color, Color},
        };

        static void Color(string k, int line)
        {
            if (Library.ColorsDic.TryGetValue(k, out Color color))
            {
                Compiler.CodeCompiler.BrushColor = color;
                return;
            }

            _ = new Exception(ExceptionType.Argument, line + 1, $"Non valid color {k}");
        }

    }
}