using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;

namespace Lexer
{
    public partial class Lexer
    {
        public static List<Token> Tokens = [];
        private static readonly string delimiters =
    @"(&&|\|\||<=|>=|==|<-|!=|\+{1,2}|\*\*|[()+*\/%&|^!<>=?:,\[\]{}"" #])";
        public static void InitializeLex(string[] code)
        {
            Tokens.Clear();
            int count = 0;

            foreach (string line in code)
            {
                LineAnalisis(line, count);
                count++;
            }
        }

        private static void LineAnalisis(string codeLine, int line)
        {
            string input = codeLine;
            string[] tokens = [.. Regex.Split(input, delimiters).Where(token =>
            !string.IsNullOrWhiteSpace(token))];

            TokenClasification(tokens, line);
        }

        private static void TokenClasification(string[] tokens, int line)
        {
            if (tokens.Length == 0) { return; }
            if (tokens[0] == "#" && tokens[^1] == "#")
            {
                //ignore comments
                return;
            }
            int position = 0;
            foreach (string token in tokens)
            {
                if (TokenTypes.operators.Contains(token))
                {
                    _ = new Token(TokenType.Operator, token, line, position);
                    continue;
                }
                if (TokenTypes.delimiters.Contains(token))
                {
                    _ = new Token(TokenType.Delimiter, token, line, position);
                    continue;
                }
                if (int.TryParse(token, out int tokenNumber))
                {
                    _ = new Token(TokenType.Number, token, line, position);
                    continue;
                }
                if (TokenTypes.keyWords.Contains(token))
                {
                    _ = new Token(TokenType.KeyWord, token, line, position);
                    continue;
                }

                _ = new Token(TokenType.Identifier, token, line, position);

                position++;
            }
        }
    }
}