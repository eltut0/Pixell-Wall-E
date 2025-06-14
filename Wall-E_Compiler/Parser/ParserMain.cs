using System.Collections.Generic;
using Godot;
using Lexer;
using ParserLibrary;
namespace Parser
{
    public class Parser
    {
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

            if (fullCompilation)
            {
                //call interpreter
            }

            // foreach (GenericNode aSTElement in AST)
            // {
            //     PrintTree(aSTElement);
            // }
        }

        // public static void PrintTree(GenericNode node, string indent = "", bool isLast = true)
        // {
        //     if (node == null) return;

        //     if (node.GetType() == typeof(AssignmentNode))
        //     {
        //         GD.Print(node.Lex + node.GetType());
        //         PrintTree(((AssignmentNode)node).VarValue);
        //     }

        //     // Imprimir el nodo actual
        //     GD.Print(indent + (isLast ? "└─ " : "├─ ") + $"{node.Lex} ({node.GetType().Name})");

        //     // Verificar si tiene hijos
        //     if (node.Children == null || node.Children.Count == 0)
        //         return;

        //     // Recorrer hijos
        //     for (int i = 0; i < node.Children.Count; i++)
        //     {
        //         bool childIsLast = (i == node.Children.Count - 1);
        //         PrintTree(
        //             node.Children[i],
        //             indent + (isLast ? "    " : "│   "),
        //             childIsLast
        //         );
        //     }
        // }
    }
}