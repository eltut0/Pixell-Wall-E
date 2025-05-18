namespace Lexer
{
    class TokenTypes
    {

        public static readonly string[] operators =
        {"=", "+", "-", "/", "*", "**", "%", "&&", "||", "==", ">", "<", "<=", ">=", "<-"};

        public static readonly string[] delimiters =
        {"(", ")", ",", "[", "]", "\"", "#"};

        public static readonly string[] keyWords =
        {"GoTo"};
    }
}