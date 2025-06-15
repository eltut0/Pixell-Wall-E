using System;
using System.Collections.Generic;

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
                if (!(arg.GetType() == typeof(Variable) || arg.GetType() == typeof(ArithmeticOperatorNode)))
                {
                    _ = new Exception(ExceptionType.Argument, Line, $"Non valid argument");
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
            throw new NotImplementedException();
        }
    }
}