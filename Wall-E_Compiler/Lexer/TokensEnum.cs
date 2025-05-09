using System.Collections.Generic;

namespace Lexer
{
    partial class Lexer
    {
        public static List<Token> Tokens = new List<Token>();
    }

    class Token
    {
        public TokenType TokenType { get; private set; }
        public string Lex { get; private set; }
        public int Line { get; private set; }
        public int Column { get; private set; }

        Token(TokenType tokenType, string lex, int line, int column)
        {
            TokenType = tokenType;
            Lex = lex;
            Line = line;
            Column = column;
            Lexer.Tokens.Add(this);
        }
    }

    enum TokenType
    {
        KeyWord,
        Identifier,
        Operator,
        Number,
        String,
        Delimiter,
    }
}