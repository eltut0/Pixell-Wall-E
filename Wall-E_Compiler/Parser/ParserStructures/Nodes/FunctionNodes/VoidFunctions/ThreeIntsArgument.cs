using System;
using System.Collections.Generic;

namespace Parser
{
    class ThreeIntsArgument(string lex, int line, FunctionType functionType, List<GenericNode> arguments) : GenericFunction(lex, line, functionType, arguments)
    {
        private delegate void Operation(int x, int y, int z);
        private readonly Operation _operation = _operations[functionType];
        protected override void SpecialValidation()
        {
            foreach (var arg in Arguments)
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
            foreach (var arg in Arguments)
            {
                arg.ExecuteNode();
            }
            _operation(((ArithmeticOperatorNode)Arguments[0]).Result, ((ArithmeticOperatorNode)Arguments[1]).Result, ((ArithmeticOperatorNode)Arguments[2]).Result);
        }
        private static readonly Dictionary<FunctionType, Operation> _operations = new()
        {
            {FunctionType.DrawLine, DrawLine},
            {FunctionType.DrawCircle, DrawCircle},
        };

        static void DrawLine(int dirX, int dirY, int distance)
        {
            throw new NotImplementedException();
        }
        static void DrawCircle(int dirX, int dirY, int radius)
        {
            throw new NotImplementedException();
        }

    }
}