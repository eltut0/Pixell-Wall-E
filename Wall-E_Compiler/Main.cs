using System.Linq;
using System.Text;
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

            foreach (var ast in AST)
            {
                GD.Print(PrintTree(ast));
            }

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

        public static string PrintTree(GenericNode node, string indent = "", bool isLast = true, bool isRoot = true)
        {
            StringBuilder sb = new StringBuilder();

            // Símbolos para la estructura del árbol
            string currentIndent = isRoot ? "" : (isLast ? "└─ " : "├─ ");
            string childIndent = isRoot ? "" : (isLast ? "   " : "│  ");

            // Información del nodo actual
            sb.Append(indent);
            sb.Append(currentIndent);

            if (node is ArithmeticOperatorNode arithNode)
            {
                sb.Append($"[OP: {arithNode.Lex}] → {GetOperatorName(arithNode.Operation)}");
                if (arithNode.Children.Count == 0)
                {
                    sb.Append($" (Valor: {arithNode.Lex})");
                }
            }
            else if (node is Variable)
            {
                sb.Append($"[VAR: {node.Lex}]");
            }
            else
            {
                sb.Append($"[NODO: {node.Lex}]");
            }

            sb.AppendLine();

            // Recursión para los hijos (usando node.Children)
            for (int i = 0; i < node.Children.Count; i++)
            {
                bool lastChild = (i == node.Children.Count - 1);
                sb.Append(PrintTree(node.Children[i], indent + childIndent, lastChild, false));
            }

            return sb.ToString();
        }

        private static string GetOperatorName(ArithmeticOperatorNode.ArithmeticOperator op)
        {
            if (op == ArithmeticOperatorNode.Sum) return "Suma";
            if (op == ArithmeticOperatorNode.Subtract) return "Resta";
            if (op == ArithmeticOperatorNode.Multiply) return "Multiplicación";
            if (op == ArithmeticOperatorNode.Divide) return "División";
            if (op == ArithmeticOperatorNode.Modulo) return "Módulo";
            if (op == ArithmeticOperatorNode.Pow) return "Potencia";
            return "Desconocido";
        }
    }
}
