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
            if (fullCompilation)
            {
                ProcesedAST = [.. AST];
            }
        }
    }
}