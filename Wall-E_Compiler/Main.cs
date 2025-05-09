namespace Compiler
{
    public static class CodeCompiler
    {
        public static void RegularCompilationStart(string[] code)
        {
            Lexer.Lexer.InitializeLex(code);
        }
    }
}