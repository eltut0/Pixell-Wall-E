using System;
using System.Collections.Generic;
using System.Linq;

namespace Parser
{
    class OneIntArgumentReturn(string lex, int line, FunctionType functionType, List<GenericNode> arguments) : GenericFunction(lex, line, functionType, arguments)
    {
        protected delegate int Operation(int x);
        private readonly Operation _Operation = _operations[functionType];
        private static readonly Dictionary<FunctionType, Operation> _operations = new()
        {
            {FunctionType.IsBrushSize, IsBrushSize},
        };
        protected override void SpecialValidation()
        {
            foreach (var arg in Children)
            {
                if (!(arg.GetType() == typeof(Variable) || arg.GetType() == typeof(ArithmeticOperatorNode) || ParserLibrary.Library.ReturnFunctions.Contains(Children[0].Lex)))
                {
                    _ = new Exception(ExceptionType.Argument, Line + 1, $"Non valid argument");
                    return;
                }
            }
        }
        public override void ExecuteNode()
        {
            Children[0].ExecuteNode();
            Result = _Operation(Children[0].Result);
        }

        static int IsBrushSize(int k)
        {
            if (Compiler.CodeCompiler.BrushSize == k) { return 1; }
            return 0;
        }
    }
}