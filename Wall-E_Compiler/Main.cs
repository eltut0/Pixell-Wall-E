using Godot;

namespace Compiler
{
    public static class CodeCompiler
    {
        public static void RegularCompilationStart(string[] code)
        {
            Lexer.Lexer.InitializeLex(code);
            Parser.Parser.ParserInit(true);
        }
    }
}