using System;
using System.Collections.Generic;

namespace Parser
{
    class OneIntArgument(string lex, int line, FunctionType functionType, List<GenericNode> arguments) : GenericFunction(lex, line, functionType, arguments)
    {
        private delegate void Operation(int x);
        private readonly Operation _operation = _operations[functionType];
        protected override void SpecialValidation()
        {
            if (!(Arguments[0].GetType() == typeof(Variable) || Arguments[0].GetType() == typeof(ArithmeticOperatorNode)))
            {
                _ = new Exception(ExceptionType.Argument, Line, $"Non valid argument");
                return;
            }
        }
        public override void ExecuteNode()
        {
            Arguments[0].ExecuteNode();
            _operation(((ArithmeticOperatorNode)Arguments[0]).Result);
        }
        private static readonly Dictionary<FunctionType, Operation> _operations = new()
        {
            {FunctionType.Size, Size},
        };

        static void Size(int k)
        {
            throw new NotImplementedException();
        }

    }
}