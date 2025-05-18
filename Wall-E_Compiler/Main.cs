using Godot;

namespace Compiler
{
    public static class CodeCompiler
    {
        public static void RegularCompilationStart(string[] code)
        {
            Lexer.Lexer.InitializeLex(code);

            foreach (var codeItem in Lexer.Lexer.Tokens)
            {
                GD.Print($"{codeItem.TokenType}: {codeItem.Lex}");
            }
        }
    }
}