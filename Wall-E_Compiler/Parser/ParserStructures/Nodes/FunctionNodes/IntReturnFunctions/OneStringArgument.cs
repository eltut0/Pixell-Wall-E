using System;
using System.Collections.Generic;

namespace Parser
{
    class OneStringArgumentReturn(string lex, int line, FunctionType functionType, List<GenericNode> arguments) : GenericFunction(lex, line, functionType, arguments)
    {
        public int Result { get; private set; }
        protected delegate int Operation(string x);
        private readonly Operation _Operation = _operations[functionType];
        private static readonly Dictionary<FunctionType, Operation> _operations = new()
        {
            {FunctionType.IsBrushColor, IsBrushColor},
        };
        protected override void SpecialValidation()
        {
            foreach (var arg in Arguments)
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
            Arguments[0].ExecuteNode();
            Result = _Operation(Arguments[0].Lex);
        }

        static int IsBrushColor(string x)
        {
            throw new NotImplementedException();
        }
    }
}