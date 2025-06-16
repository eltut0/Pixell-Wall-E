using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

namespace Parser
{
    class FiveIntsArgument(string lex, int line, FunctionType functionType, List<GenericNode> arguments) : GenericFunction(lex, line, functionType, arguments)
    {
        private delegate void Operation(int v, int w, int x, int y, int z);
        private readonly Operation _operation = _operations[functionType];
        protected override void SpecialValidation()
        {
            foreach (var arg in Children)
            {
                if (!(arg.GetType() == typeof(Variable) || arg.GetType() == typeof(ArithmeticOperatorNode) || ParserLibrary.Library.ReturnFunctions.Contains(arg.Lex)))
                {
                    _ = new Exception(ExceptionType.Argument, Line, $"Non valid argument");
                    return;
                }
            }
        }
        public override void ExecuteNode()
        {
            foreach (var arg in Children)
            {
                arg.ExecuteNode();
            }
            _operation(((ArithmeticOperatorNode)Children[0]).Result, ((ArithmeticOperatorNode)Children[1]).Result, ((ArithmeticOperatorNode)Children[2]).Result,
            ((ArithmeticOperatorNode)Children[3]).Result, ((ArithmeticOperatorNode)Children[4]).Result);
        }
        private static readonly Dictionary<FunctionType, Operation> _operations = new()
        {
            {FunctionType.DrawRectangle, DrawRectangle},
        };

        public static void DrawRectangle(int dirX, int dirY, int distance, int width, int height)
        {
            if (Math.Abs(dirX) > 1 || Math.Abs(dirY) > 1 || (dirX == 0 && dirY == 0))
            {
                _ = new Exception(ExceptionType.Argument, -1, "Invalid direction parameters");
                return;
            }

            if (width <= 0 || height <= 0)
            {
                _ = new Exception(ExceptionType.Argument, -1, "Width and height must be positive");
                return;
            }

            int currentX = Compiler.CodeCompiler.XPosition;
            int currentY = Compiler.CodeCompiler.YPosition;
            int centerX = currentX + dirX * distance;
            int centerY = currentY + dirY * distance;

            int canvasSize = GlobalParameters.ProjectGlobalParameters.CanvasSize;
            if (centerX < 0 || centerX >= canvasSize || centerY < 0 || centerY >= canvasSize)
            {
                _ = new Exception(ExceptionType.Argument, -1, "Rectangle center out of canvas bounds");
                return;
            }

            int halfWidth = width / 2;
            int halfHeight = height / 2;

            int left = Math.Max(0, centerX - halfWidth);
            int right = Math.Min(canvasSize - 1, centerX + halfWidth);
            int top = Math.Max(0, centerY - halfHeight);
            int bottom = Math.Min(canvasSize - 1, centerY + halfHeight);

            int originalX = currentX;
            int originalY = currentY;
            Color originalColor = Compiler.CodeCompiler.BrushColor;

            Compiler.CodeCompiler.XPosition = left;
            Compiler.CodeCompiler.YPosition = top;
            ThreeIntsArgument.DrawLine(1, 0, right - left);

            Compiler.CodeCompiler.XPosition = right;
            Compiler.CodeCompiler.YPosition = top;
            ThreeIntsArgument.DrawLine(0, 1, bottom - top);

            Compiler.CodeCompiler.XPosition = right;
            Compiler.CodeCompiler.YPosition = bottom;
            ThreeIntsArgument.DrawLine(-1, 0, right - left);

            Compiler.CodeCompiler.XPosition = left;
            Compiler.CodeCompiler.YPosition = bottom;
            ThreeIntsArgument.DrawLine(0, -1, bottom - top);

            Compiler.CodeCompiler.XPosition = centerX;
            Compiler.CodeCompiler.YPosition = centerY;
            Compiler.CodeCompiler.BrushColor = originalColor;
        }

    }
}