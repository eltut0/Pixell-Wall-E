using Godot;

namespace Parser
{
    public static class FunctionAuxMethods
    {
        public static void DrawPixel()
        {
            Color color = Compiler.CodeCompiler.BrushColor;
            int centerX = Compiler.CodeCompiler.XPosition;
            int centerY = Compiler.CodeCompiler.YPosition;
            int halfSize = Compiler.CodeCompiler.BrushSize / 2;

            for (int offsetX = -halfSize; offsetX <= halfSize; offsetX++)
            {
                for (int offsetY = -halfSize; offsetY <= halfSize; offsetY++)
                {
                    if (IsWithinCanvas(centerX + offsetX, centerY + offsetY))
                    {

                        SetPixelColor(centerX + offsetX, centerY + offsetY, color);
                    }
                }
            }
        }

        static void SetPixelColor(int x, int y, Color color)
        {
            if (color == Colors.Transparent) { return; }
            Compiler.CodeCompiler.CanvasMatrix[x, y] = color;
        }
        static bool IsWithinCanvas(int x, int y)
        {
            int canvasSize = GlobalParameters.ProjectGlobalParameters.CanvasSize;
            return x >= 0 && x < canvasSize && y >= 0 && y < canvasSize;
        }
    }
}