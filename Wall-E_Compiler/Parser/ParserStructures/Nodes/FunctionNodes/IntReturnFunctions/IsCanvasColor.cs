using System;
using System.Collections.Generic;

namespace Parser
{
    class IsCanvasColor(string lex, int line, FunctionType functionType, List<GenericNode> arguments) : GenericFunction(lex, line, functionType, arguments)
    {
        protected override void SpecialValidation()
        {
            GenericNode[] temp = [.. Children];
            if (!temp[0].IsString)
            {
                _ = new Exception(ExceptionType.Argument, Line + 1, "First argument requires type string");
            }
            for (int i = 1; i < temp.Length; i++)
            {
                if (!(temp[i].GetType() == typeof(Variable) || temp[i].GetType() == typeof(ArithmeticOperatorNode)))
                {
                    _ = new Exception(ExceptionType.Argument, Line + 1, $"Non valid {i} argument");
                    return;
                }
            }
        }
        public override void ExecuteNode()
        {
            foreach (var node in Children) { node.ExecuteNode(); }
            Result = IsCanvasColorFunc(Children[0].Lex, Children[1].Result, Children[2].Result);
        }
        private static int IsCanvasColorFunc(string color, int x, int y)
        {
            if (Compiler.CodeCompiler.CanvasMatrix[x, y] == ParserLibrary.Library.ColorsDic[color]) { return 1; }
            return 0;
        }
    }
}