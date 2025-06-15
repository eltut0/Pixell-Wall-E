using System.Collections.Generic;
using Godot;
using Lexer;
using ParserLibrary;
namespace Parser
{
    public class Parser
    {
        public static GenericNode[] ProcesedAST { get; private set; }
        public static void ParserInit(bool fullCompilation)
        {
            Library.ClearData();
            Token[][] convertedList = Library.ConvertListToArray(Lexer.Lexer.Tokens);
            List<GenericNode> AST = [];

            if (convertedList.Length == 0) { return; }

            AST.Add(Library.SpecialFirstLine(convertedList[0]));

            for (int i = 1; i < convertedList.Length; i++)
            {
                GenericNode aSTElement = Library.ConvertLineToTree(convertedList[i]);
                AST.Add(aSTElement);
            }

            // foreach (GenericNode aSTElement in AST)
            // {
            //     if (aSTElement is GoToJump jump)
            //     {
            //         PrintTree(jump.Condition, "");
            //     }
            // }
            if (fullCompilation)
            {
                ProcesedAST = [.. AST];
            }
        }

        public static void PrintTree(GenericNode node, string indent = "", bool isLast = true)
        {
            if (node == null) return;

            // Imprimir el nodo actual
            GD.Print(indent + (isLast ? "└─ " : "├─ ") + $"{node.Lex} ({node.GetType().Name})");

            // Caso especial para nodos GoTo
            if (node is GoToJump gotoNode)
            {
                GD.Print(indent + (isLast ? "    " : "│   ") + "├─ Label: " + gotoNode.Label);
                if (gotoNode.Condition != null)
                {
                    GD.Print(indent + (isLast ? "    " : "│   ") + "└─ Condition:");
                    PrintTree(gotoNode.Condition, indent + (isLast ? "        " : "│       "), true);
                }
                return;
            }

            // Caso especial para nodos de asignación
            if (node is AssignmentNode assignmentNode)
            {
                GD.Print(indent + (isLast ? "    " : "│   ") + "├─ Variable: " + assignmentNode.Lex);
                GD.Print(indent + (isLast ? "    " : "│   ") + "└─ Value:");
                PrintTree(assignmentNode.VarValue, indent + (isLast ? "        " : "│       "), true);
                return;
            }

            if (node is ComparisonNode comparisonNode)
            {
                GD.Print(indent + (isLast ? "    " : "│   ") + "├─ Left:");
                PrintTree(comparisonNode.Left, indent + (isLast ? "    │   " : "│   │   "), false);

                GD.Print(indent + (isLast ? "    " : "│   ") + "└─ Right:");
                PrintTree(comparisonNode.Right, indent + (isLast ? "        " : "│       "), true);
                return;
            }

            // Caso especial para nodos de operación aritmética
            if (node is ArithmeticOperatorNode arithNode)
            {
                // Imprimir el operador (primer hijo)
                if (arithNode.Children.Count > 0)
                {
                    GD.Print(indent + (isLast ? "    " : "│   ") + "├─ Operator: " + arithNode.Children[0].Lex);
                }

                // Imprimir los operandos (hijos restantes)
                for (int i = 1; i < arithNode.Children.Count; i++)
                {
                    bool operandIsLast = (i == arithNode.Children.Count - 1);
                    GD.Print(indent + (isLast ? "    " : "│   ") + (operandIsLast ? "└─ " : "├─ ") + "Operand " + i + ":");
                    PrintTree(arithNode.Children[i], indent + (isLast ? "    " : "│   ") + (operandIsLast ? "   " : "│  "), operandIsLast);
                }
                return;
            }

            // Procesamiento normal para otros nodos con hijos
            if (node.Children == null || node.Children.Count == 0)
                return;

            // Recorrer hijos
            for (int i = 0; i < node.Children.Count; i++)
            {
                bool childIsLast = (i == node.Children.Count - 1);
                PrintTree(
                    node.Children[i],
                    indent + (isLast ? "    " : "│   "),
                    childIsLast
                );
            }
        }
    }
}