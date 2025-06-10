using Lexer;
using Parser;
using System.Collections.Generic;
using System.Linq;

namespace ParserLibrary
{
    public static partial class Library
    {
        private static GenericNode[] ProcessFunctionArgument(Token[] tokens)
        {
            Token[][] args = SplitArguments(tokens);
            List<GenericNode> builtArgs = [];

            foreach (Token[] token in args)
            {
                builtArgs.Add(BuildArgument(token));
            }

            return [.. builtArgs];
        }

        private static GenericNode BuildArgument(Token[] args)
        {
            if (args.Any(c => c.Lex == "||" || args.Any(c => c.Lex == "&&")))
            {
                _ = new Exception(ExceptionType.Argument, args[0].Line + 1, "Not admited boolean expressions");
                return null;
            }

            if (args.Any(c => c.Lex == "**"))
            {

            }

            return null;
        }
    }
}