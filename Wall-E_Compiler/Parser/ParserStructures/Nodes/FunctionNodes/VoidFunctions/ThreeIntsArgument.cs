using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

namespace Parser
{
    class ThreeIntsArgument(string lex, int line, FunctionType functionType, List<GenericNode> arguments) : GenericFunction(lex, line, functionType, arguments)
    {
        private delegate void Operation(int x, int y, int z, int line);
        private readonly Operation _operation = _operations[functionType];
        protected override void SpecialValidation()
        {
            foreach (var arg in Children)
            {
                if (!(arg.GetType() == typeof(Variable) || arg.GetType() == typeof(ArithmeticOperatorNode) || ParserLibrary.Library.ReturnFunctions.Contains(arg.Lex)))
                {
                    _ = new Exception(ExceptionType.Argument, Line + 1, $"Non valid argument");
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
            _operation(Children[0].Result, Children[1].Result, Children[2].Result, Line);
        }
        private static readonly Dictionary<FunctionType, Operation> _operations = new()
        {
            {FunctionType.DrawLine, DrawLine},
            {FunctionType.DrawCircle, DrawCircle},
        };

        public static void DrawLine(int dirX, int dirY, int distance, int line)
        {
            if (Math.Abs(dirX) > 1 || Math.Abs(dirY) > 1 || (dirX == 0 && dirY == 0))
            {
                _ = new Exception(ExceptionType.Argument, line + 1, "Non valid argument for DrawLine");
                return;
            }

            for (int i = 0; i < distance; i++)
            {
                DrawPixelAt(Compiler.CodeCompiler.XPosition, Compiler.CodeCompiler.YPosition);
                Compiler.CodeCompiler.XPosition += dirX;
                Compiler.CodeCompiler.YPosition += dirY;

                if (IsOutsideCanvas(Compiler.CodeCompiler.XPosition + dirX) && IsOutsideCanvas(Compiler.CodeCompiler.YPosition + dirY))
                {
                    return;
                }
            }
        }
        public static void DrawCircle(int dirX, int dirY, int radius, int line)
        {
            if (Math.Abs(dirX) > 1 || Math.Abs(dirY) > 1 || radius <= 0)
            {
                _ = new Exception(ExceptionType.Argument, line + 1, "Non valid argument for DrawCircle");
                return;
            }

            int centerX = Compiler.CodeCompiler.XPosition + dirX * radius;
            int centerY = Compiler.CodeCompiler.YPosition + dirY * radius;

            int canvasSize = GlobalParameters.ProjectGlobalParameters.CanvasSize;
            if (centerX < 0 || centerX >= canvasSize || centerY < 0 || centerY >= canvasSize)
            {
                _ = new Exception(ExceptionType.Argument, line + 1, "Circle center out of canvas bounds");
                return;
            }

            int x = 0;
            int y = radius;
            int d = 3 - 2 * radius;

            while (y >= x)
            {
                DrawCirclePoints(centerX, centerY, x, y);

                x++;
                if (d > 0)
                {
                    y--;
                    d += 4 * (x - y) + 10;
                }
                else
                {
                    d += 4 * x + 6;
                }
            }

            Compiler.CodeCompiler.XPosition = centerX;
            Compiler.CodeCompiler.YPosition = centerY;
        }

        private static void DrawCirclePoints(int cx, int cy, int x, int y)
        {
            DrawPixelAt(cx + x, cy + y);
            DrawPixelAt(cx - x, cy + y);
            DrawPixelAt(cx + x, cy - y);
            DrawPixelAt(cx - x, cy - y);
            DrawPixelAt(cx + y, cy + x);
            DrawPixelAt(cx - y, cy + x);
            DrawPixelAt(cx + y, cy - x);
            DrawPixelAt(cx - y, cy - x);
        }

        private static void DrawPixelAt(int x, int y)
        {
            int canvasSize = GlobalParameters.ProjectGlobalParameters.CanvasSize;
            if (x >= 0 && x < canvasSize && y >= 0 && y < canvasSize)
            {
                Compiler.CodeCompiler.XPosition = x;
                Compiler.CodeCompiler.YPosition = y;
                FunctionAuxMethods.DrawPixel();
            }
        }

        private static bool IsOutsideCanvas(int x)
        {
            if (x < 0 || x >= GlobalParameters.ProjectGlobalParameters.CanvasSize) return true;

            return false;
        }

    }
}