using System.Collections.Generic;
using System.Linq;
using Lexer;
using Parser;
namespace ParserLibrary
{
    public static partial class Library
    {
        public static void ClearData()
        {
            Exception.exceptionList.Clear();
            Variable.VariablesDic.Clear();
            MyLabel.Labels.Clear();
        }

        public static Token[][] ConvertListToArray(List<Token> list)
        {
            return [.. list
            .GroupBy(o => o.Line)
            .OrderBy(g => g.Key)
            .Select(g => g.ToArray())];
        }

        private static Token[][] SplitArguments(Token[] args)
        {
            List<List<Token>> temp = [[],];

            foreach (Token token in args)
            {
                if (token.Lex == ",")
                {
                    temp.Add([]);
                }
                else
                {
                    temp.Last().Add(token);
                }
            }

            return [.. temp.Where(sub => sub.Count > 0).Select(sub => sub.ToArray())];
        }
    }
}