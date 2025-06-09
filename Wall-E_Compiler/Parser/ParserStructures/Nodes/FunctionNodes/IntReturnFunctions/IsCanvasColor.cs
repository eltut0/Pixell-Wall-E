using System;
using System.Collections.Generic;

namespace Parser
{
    class IsCanvasColor(string lex, int line, FunctionType functionType, List<GenericNode> arguments) : GenericFunction(lex, line, functionType, arguments)
    {
        public int Result { get; private set; }
        protected override void SpecialValidation()
        {
            GenericNode[] temp = [.. Arguments];
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
            foreach (var node in Arguments) { node.ExecuteNode(); }
            Result = IsCanvasColorFunc(Arguments[0].Lex, ((ArithmeticOperatorNode)Arguments[1]).Result, ((ArithmeticOperatorNode)Arguments[2]).Result);
        }
        private static int IsCanvasColorFunc(string color, int x, int y)
        {
            throw new NotImplementedException();
        }
    }
}