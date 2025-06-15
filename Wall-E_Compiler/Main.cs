using System.Linq;
using Canvas;
using Godot;
using Parser;

namespace Compiler
{
    public static class CodeCompiler
    {
        public static int XPosition { get; set; }
        public static int YPosition { get; set; }
        public static Color BrushColor { get; set; }
        public static Color[,] CanvasMatrix { get; set; }
        public static int BrushSize { get; set; }
        public static void RegularCompilationStart(string[] code)
        {
            Lexer.Lexer.InitializeLex(code);
            Parser.Parser.ParserInit(true);
            if (Exception.exceptionList.Count == 0)
            {
                InterpretateCode();
            }
        }

        private static void InterpretateCode()
        {
            //re initialize parameterss
            BrushColor = Colors.Transparent;
            CanvasMatrix = new Color[GlobalParameters.ProjectGlobalParameters.CanvasSize, GlobalParameters.ProjectGlobalParameters.CanvasSize];
            BrushSize = 1;

            for (int i = 0; i < CanvasMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < CanvasMatrix.GetLength(0); j++)
                {
                    if (i == j)
                    {
                        CanvasMatrix[i, j] = Colors.Blue;
                        continue;
                    }
                    CanvasMatrix[i, j] = Colors.Red;
                }
            }



            GenericNode[] AST = Parser.Parser.ProcesedAST;
            for (int i = 0; i < AST.Length; i++)
            {
                if (Exception.exceptionList.Count > 0)
                {
                    InfoWindow.DisplayInfoWindow("Exception during the execution", $"{Exception.exceptionList[0].Type}, {Exception.exceptionList[0].Line}, {Exception.exceptionList[0].Lex}", 400, 100);
                    break;
                }
                //conditional jump case
                if (AST[i].GetType() == typeof(GoToJump))
                {
                    AST[i].ExecuteNode();
                    if (((GoToJump)AST[i]).ValidJump() && MyLabel.Labels.Any(x => x.Lex == ((GoToJump)AST[i]).Label.Lex))
                    {
                        i = ((GoToJump)AST[i]).Label.Line;
                    }
                    continue;
                }

                AST[i].ExecuteNode();
            }

            var tempNode = new Node();
            // Obtener el árbol de la escena actual (funciona si se llama desde MainLoop)
            SceneTree arbol = Engine.GetMainLoop() as SceneTree;
            arbol.Root.AddChild(tempNode);
            Canvas.Canvas canvas = tempNode.GetNode<Canvas.Canvas>(GlobalParameters.ProjectGlobalParameters.CanvasNode);
            canvas.SetColors(CanvasMatrix);
            tempNode.QueueFree();
        }
    }
}
