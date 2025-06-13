using System.Collections.Generic;
using Godot;
using Lexer;
using ParserLibrary;
namespace Parser
{
    public class Parser
    {
        public static void ParserInit()
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

            //call interpreter
        }
    }
}