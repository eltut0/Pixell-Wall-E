using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;

namespace Lexer
{
    partial class Lexer
    {
        public static List<Token> Tokens = new List<Token>();
        private static readonly string delimitters = @"([(),""\[\] ])";
        public static void InitializeLex(string[] code)
        {
            Tokens.Clear();
            int count = 0;
            foreach (string line in code)
            {
                LineAnalisis(line, count);
            }
        }

        private static void LineAnalisis(string codeLine, int line)
        {
            string input = codeLine;
            string[] tokens = Regex.Split(input, delimitters).Where(token =>
            !string.IsNullOrWhiteSpace(token)).ToArray();

            TokenClasification(tokens, line);
        }

        private static void TokenClasification(string[] tokens, int line)
        {
            int position = 0;
            foreach (string token in tokens)
            {
                //check type
                if (TokenTypes.operators.Contains(token))
                {
                    new Token(TokenType.Operator, token, line, position);
                    continue;
                }
                if (TokenTypes.delimiters.Contains(token))
                {
                    new Token(TokenType.Delimiter, token, line, position);
                    continue;
                }
                if (int.TryParse(token, out int tokenNumber))
                {
                    new Token(TokenType.Number, token, line, position);
                    continue;
                }
                if (TokenTypes.keyWords.Contains(token))
                {
                    new Token(TokenType.KeyWord, token, line, position);
                    continue;
                }

                new Token(TokenType.Identifier, token, line, position);
            }
        }
    }
}