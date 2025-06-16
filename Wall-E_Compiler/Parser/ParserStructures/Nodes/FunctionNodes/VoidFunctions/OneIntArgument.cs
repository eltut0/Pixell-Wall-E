using System;
using System.Collections.Generic;
using System.Linq;

namespace Parser
{
    class OneIntArgument(string lex, int line, FunctionType functionType, List<GenericNode> arguments) : GenericFunction(lex, line, functionType, arguments)
    {
        private delegate void Operation(int x);
        private readonly Operation _operation = _operations[functionType];
        protected override void SpecialValidation()
        {
            if (!(Children[0].GetType() == typeof(Variable) || Children[0].GetType() == typeof(ArithmeticOperatorNode) || ParserLibrary.Library.ReturnFunctions.Contains(Children[0].Lex)))
            {
                _ = new Exception(ExceptionType.Argument, Line, $"Non valid argument");
                return;
            }
        }
        public override void ExecuteNode()
        {
            Children[0].ExecuteNode();
            _operation(((ArithmeticOperatorNode)Children[0]).Result);
        }
        private static readonly Dictionary<FunctionType, Operation> _operations = new()
        {
            {FunctionType.Size, Size},
        };

        static void Size(int k)
        {
            if (k % 2 == 0)
            {
                k--;
                if (k < 1) k = 1;
            }
            else
            {
                if (k < 1) k = 1;
            }

            Compiler.CodeCompiler.BrushSize = k;
        }

    }
}