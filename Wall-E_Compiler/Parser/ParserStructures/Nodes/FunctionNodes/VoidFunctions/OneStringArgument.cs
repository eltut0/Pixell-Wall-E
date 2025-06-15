using System;
using System.Collections.Generic;

namespace Parser
{
    class OneStringArgument(string lex, int line, FunctionType functionType, List<GenericNode> arguments) : GenericFunction(lex, line, functionType, arguments)
    {
        private delegate void Operation(string x);
        private readonly Operation _operation = _operations[functionType];
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
            _operation(Children[0].Lex);
        }
        private static readonly Dictionary<FunctionType, Operation> _operations = new()
        {
            {FunctionType.Color, Color},
        };

        static void Color(string k)
        {
            throw new NotImplementedException();
        }

    }
}