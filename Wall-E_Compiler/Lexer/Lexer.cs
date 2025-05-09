namespace Lexer
{
    partial class Lexer
    {
        public static void InitializeLex(string[] code)
        {
            Tokens.Clear();
            for (int i = 0; i < code.Length; i++)
            {
                LineAnalisis(code[i]);
            }
        }

        private static void LineAnalisis(string codeLine)
        {

        }
    }
}