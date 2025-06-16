using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using ParserLibrary;

namespace Parser
{
    class GetColorCount(string lex, int line, FunctionType functionType, List<GenericNode> arguments) : GenericFunction(lex, line, functionType, arguments)
    {
        protected override void SpecialValidation()
        {
            GenericNode[] temp = [.. Children];
            if (!temp[0].IsString)
            {
                _ = new Exception(ExceptionType.Argument, Line, "First argument requires type string");
            }
            for (int i = 1; i < temp.Length; i++)
            {
                if (!(temp[i].GetType() == typeof(Variable) || temp[i].GetType() == typeof(ArithmeticOperatorNode) || ParserLibrary.Library.ReturnFunctions.Contains(Children[0].Lex)))
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
            int lim = GlobalParameters.ProjectGlobalParameters.CanvasSize;
            if (x1 < 0 || x1 >= lim || y1 < 0 || y1 >= lim || x2 < 0 || x2 >= lim || y2 < 0 || y2 >= lim)
            {
                _ = new Exception(ExceptionType.Argument, -1, "Limits out of the canvas");
                return -1;
            }

            Color thisColor = Colors.Transparent;

            if (Library.ColorsDic.TryGetValue(color, out Color value))
            {
                thisColor = value;
            }
            else
            {
                _ = new Exception(ExceptionType.Argument, -1, "Non valid color");
                return -1;
            }

            int count = 0;

            for (int i = x1; i <= x2; i++)
            {
                for (int j = y1; j <= y2; j++)
                {
                    if (Compiler.CodeCompiler.CanvasMatrix[i, j] == thisColor)
                    {
                        count++;
                    }
                }
            }

            return count;
        }
    }
}