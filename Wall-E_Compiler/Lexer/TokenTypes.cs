namespace Lexer
{
    public class TokenTypes
    {

        public static readonly string[] operators =
        {"=", "+", "-", "/", "*", "**", "%", "&&", "||", "==", ">", "<", "<=", ">=", "<_"};

        public static readonly string[] delimiters =
        {"(", ")", ",", "[", "]", "\"", "#"};

        public static readonly string[] keyWords =
        {"GoTo"};
    }
}