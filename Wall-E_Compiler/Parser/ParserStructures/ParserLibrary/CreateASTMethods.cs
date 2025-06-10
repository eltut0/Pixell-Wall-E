using System.Collections.Generic;
using System.Linq;
using Lexer;
using Parser;

namespace ParserLibrary
{
    public static partial class Library
    {
        public static GenericNode SpecialFirstLine(Token[] tokens)
        {
            if (tokens[0].Lex != "Spawn")
            {
                _ = new Exception(ExceptionType.LineOvercharge, tokens[0].Line + 1, "Code must initialize with an Spawn");
                return null;
            }
            if (tokens.Length < 6)
            {
                _ = new Exception(ExceptionType.Argument, tokens[0].Line + 1, "Missing arguments");
                return null;
            }
            if (tokens[1].Lex != "(" || tokens[^1].Lex != ")")
            {
                _ = new Exception(ExceptionType.Argument, tokens[0].Line + 1, "Delimiters expected");
                return null;
            }

            GenericNode[] args = ProcessFunctionArgument(tokens[1..^1]);
            //return SpawnNode
            return null;
        }
        public static GenericNode ConvertLineToTree(Token[] tokens)
        {
            if (tokens.Length == 1 && !Functions.Contains(tokens[0].Lex))
            {
                MyLabel label = new(tokens[0].Lex, tokens[0].Line);
                return label;
            }
            if (!(tokens.Length < 2))
            {
                if (tokens[1].Lex != "<_" && tokens[1].Lex != "(")
                {
                    _ = new Exception(ExceptionType.SyntaxError, tokens[0].Line + 1, "Assignation for variable expected");
                }
                if (tokens[0].TokenType == TokenType.KeyWord && tokens[0].Lex == "GoTo")
                {
                    return GoToNode(tokens);
                }
                if (tokens[0].TokenType == TokenType.Identifier && Functions.Contains(tokens[0].Lex))
                {
                    if (tokens.Length < 3 || (tokens[1].Lex != "(" && tokens[^1].Lex != ")"))
                    {
                        _ = new Exception(ExceptionType.Argument, tokens[0].Line + 1, "Delimiters expected");
                        return null;
                    }
                    GenericNode[] args = ProcessFunctionArgument(tokens[1..^1]); //ignoring function and delimiters for the tokens array

                }
            }
            else
            {
                _ = new Exception(ExceptionType.Argument, tokens[0].Line + 1, "Delimiters expected");
            }

            return null;
        }

        private static GoToJump GoToNode(Token[] tokens)
        {
            throw new System.NotImplementedException();
        }

        private static Token[][] SplitArguments(Token[] args)
        {
            List<List<Token>> temp = [[],];

            foreach (Token token in args)
            {
                if (token.Lex == ",")
                {
                    temp.Add([]);
                }
                else
                {
                    temp.Last().Add(token);
                }
            }

            return [.. temp.Where(sub => sub.Count > 0).Select(sub => sub.ToArray())];
        }
    }
}