using System;
using System.Collections.Generic;

namespace Parser
{
    class GetColorCount(string lex, int line, FunctionType functionType, List<GenericNode> arguments) : GenericFunction(lex, line, functionType, arguments)
    {
        public int Result { get; private set; }
        protected override void SpecialValidation()
        {
            GenericNode[] temp = [.. Children];
            if (!temp[0].IsString)
            {
                _ = new Exception(ExceptionType.Argument, Line, "First argument requires type string");
            }
            for (int i = 1; i < temp.Length; i++)
            {
                if (!(temp[i].GetType() == typeof(Variable) || temp[i].GetType() == typeof(ArithmeticOperatorNode)))
                {
                    _ = new Exception(ExceptionType.Argument, Line, $"Non valid {i} argument");
                    return;
                }
            }
        }
        public override void ExecuteNode()
        {
            foreach (var node in Children) { node.ExecuteNode(); }
            Result = GetColorCountFunc(Children[0].Lex, ((ArithmeticOperatorNode)Children[1]).Result, ((ArithmeticOperatorNode)Children[2]).Result,
            ((ArithmeticOperatorNode)Children[3]).Result, ((ArithmeticOperatorNode)Children[4]).Result);
        }
        private static int GetColorCountFunc(string color, int x1, int y1, int x2, int y2)
        {
            throw new NotImplementedException();
        }
    }
}