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


    }
}