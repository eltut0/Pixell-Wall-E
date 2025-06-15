using Godot;

namespace Parser
{
    public static class FunctionAuxMethods
    {
        static void DrawPixel(int centerX, int centerY, int brushSize)
        {
            Color color = Compiler.CodeCompiler.BrushColor;

            int halfSize = brushSize / 2;

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
            Compiler.CodeCompiler.CanvasMatrix[x, y] = color;
        }
        static bool IsWithinCanvas(int x, int y)
        {
            int canvasSize = GlobalParameters.ProjectGlobalParameters.CanvasSize;
            return x >= 0 && x < canvasSize && y >= 0 && y < canvasSize;
        }
    }
}