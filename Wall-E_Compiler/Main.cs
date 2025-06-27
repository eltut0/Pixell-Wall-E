using System.Linq;
using System.Threading;
using Canvas;
using Godot;
using Parser;

namespace Compiler
{
    public static class CodeCompiler
    {
        public static bool DontCheck { get; private set; }
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
            DontCheck = true;

            //re initialize parameterss
            BrushColor = Colors.Transparent;
            CanvasMatrix = new Color[GlobalParameters.ProjectGlobalParameters.CanvasSize, GlobalParameters.ProjectGlobalParameters.CanvasSize];
            BrushSize = 1;

            for (int i = 0; i < CanvasMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < CanvasMatrix.GetLength(0); j++)
                {
                    CanvasMatrix[i, j] = Colors.Transparent;
                }
            }

            GenericNode[] AST = Parser.Parser.ProcesedAST;
            int count = 0; //for infinite loops
            int k = 0;

            while (k < AST.Length)
            {
                if (AST[k] is GoToJump temp)
                {
                    MyLabel label = MyLabel.Labels.Find(x => x.Lex == temp.Label);
                    if (label == null) { _ = new Exception(ExceptionType.NotDefinedObject, temp.Line + 1, "Non defined label"); }
                    else
                    {
                        if (temp.ValidJump()) { k = label.Line; }
                    }
                }

                AST[k].ExecuteNode();

                if (Exception.exceptionList.Count > 0)
                {
                    InfoWindow.DisplayInfoWindow("Exception during the execution", $"{Exception.exceptionList[0].Type}, {k + 1}, {Exception.exceptionList[0].Lex}", 400, 100);
                    break;
                }

                count++;
                k++;
                if (count == 1000000)
                {
                    _ = new Exception(ExceptionType.LineOvercharge, -1, $"Possible infinite loop, stopped \n after {count} iterations");
                }
            }

            DontCheck = false;

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
