using System.Collections.Generic;
using Godot;

namespace Parser
{
    class NonArgument(string lex, int line, FunctionType functionType, List<GenericNode> arguments) : GenericFunction(lex, line, functionType, arguments)
    {
        protected delegate void Operation();
        private readonly Operation _Operation = _operations[functionType];
        private static readonly Dictionary<FunctionType, Operation> _operations = new()
        {
            {FunctionType.Fill, Fill},
        };

        public override void ExecuteNode()
        {
            _Operation();
        }

        public static void Fill()
        {
            Color[,] canvas = Compiler.CodeCompiler.CanvasMatrix;
            int startX = Compiler.CodeCompiler.XPosition;
            int startY = Compiler.CodeCompiler.YPosition;
            Color newColor = Compiler.CodeCompiler.BrushColor;

            if (canvas == null) return;

            int rows = canvas.GetLength(0);
            int cols = canvas.GetLength(1);

            if (startX < 0 || startX >= cols || startY < 0 || startY >= rows)
            {
                return;
            }

            Color targetColor = canvas[startY, startX];

            if (targetColor.Equals(newColor))
            {
                return;
            }

            Queue<(int x, int y)> pixels = new();
            pixels.Enqueue((startX, startY));

            while (pixels.Count > 0)
            {
                var (x, y) = pixels.Dequeue();

                if (x < 0 || x >= cols || y < 0 || y >= rows)
                {
                    continue;
                }

                if (!canvas[y, x].Equals(targetColor))
                {
                    continue;
                }

                canvas[y, x] = newColor;

                pixels.Enqueue((x + 1, y));
                pixels.Enqueue((x - 1, y));
                pixels.Enqueue((x, y + 1));
                pixels.Enqueue((x, y - 1));
            }

            Compiler.CodeCompiler.CanvasMatrix = canvas;
        }
    }
}