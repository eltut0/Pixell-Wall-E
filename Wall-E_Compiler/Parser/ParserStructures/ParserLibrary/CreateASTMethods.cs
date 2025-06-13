using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using Godot;
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

            GenericNode[] args = ProcessFunctionArgument(tokens[2..^1]);
            GenericNode gn = new(tokens[0].Lex, tokens[0].Line);
            foreach (GenericNode arg in args)
            {
                gn.AddChild(arg);
            }
            return gn;
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
                    if (tokens.Length < 3 || tokens[1].Lex != "(" || tokens[^1].Lex != ")")
                    {
                        _ = new Exception(ExceptionType.Argument, tokens[0].Line + 1, "Delimiters expected");
                        return null;
                    }
                    GenericNode[] args = ProcessFunctionArgument(tokens[2..^1]); //ignoring function and delimiters for the tokens array

                    return BuildFunction(args[0].Lex, args[0].Line, args);
                }
            }
            else
            {
                _ = new Exception(ExceptionType.Argument, tokens[0].Line + 1, "Delimiters expected");
            }

            return null;
        }

        private static GenericFunction BuildFunction(string lex, int line, GenericNode[] args)
        {
            return lex switch
            {
                "GetActualX" => new NonArgumentReturn(lex, line, FunctionType.GetActualX, null),
                "GetActualY" => new NonArgumentReturn(lex, line, FunctionType.GetActualY, null),
                "GetCanvasSize" => new NonArgumentReturn(lex, line, FunctionType.GetCanvasSize, null),
                "GetColorCount" => new NonArgumentReturn(lex, line, FunctionType.GetColorCount, [.. args]),
                "IsBrushColor" => new NonArgumentReturn(lex, line, FunctionType.IsBrushColor, [.. args]),
                "IsBrushSize" => new NonArgumentReturn(lex, line, FunctionType.IsBrushSize, [.. args]),
                "IsCanvasColor" => new NonArgumentReturn(lex, line, FunctionType.IsCanvasColor, [.. args]),
                _ => null,
            };
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