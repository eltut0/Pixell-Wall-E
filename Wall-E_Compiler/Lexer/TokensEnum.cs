using System.Collections.Generic;

namespace Lexer
{
    public class Token
    {
        public TokenType TokenType { get; private set; }
        public string Lex { get; private set; }
        public int Line { get; private set; }
        public int Position { get; private set; }

        public Token(TokenType tokenType, string lex, int line, int position)
        {
            TokenType = tokenType;
            Lex = lex;
            Line = line;
            Position = position;
            Lexer.Tokens.Add(this);
        }
    }

    public enum TokenType
    {
        KeyWord,
        Identifier,
        Operator,
        Number,
        Delimiter,
    }
}