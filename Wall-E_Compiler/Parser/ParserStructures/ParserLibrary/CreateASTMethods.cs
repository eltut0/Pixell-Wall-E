using System.Drawing;
using System.Linq;
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
            TwoIntsArgument gn = new(tokens[0].Lex, tokens[0].Line, FunctionType.Spawn, [.. args]);

            gn.ValidateArgument();

            return gn;
        }
        public static GenericNode ConvertLineToTree(Token[] tokens)
        {
            if (tokens.Length == 1 && !Functions.Contains(tokens[0].Lex))
            {
                MyLabel label = new(tokens[0].Lex, tokens[0].Line);
                label.AddList();
                return label;
            }
            if (tokens[0].Lex == "Spawn")
            {
                _ = new Exception(ExceptionType.SyntaxError, tokens[0].Line + 1, "Only one Spawn can be called");
            }
            if (!(tokens.Length < 2))
            {
                if (tokens[1].Lex != "<-" && tokens[1].Lex != "(" && tokens[1].Lex != "[")
                {
                    _ = new Exception(ExceptionType.SyntaxError, tokens[0].Line + 1, "Assignation for variable expected");
                }
                if (tokens[0].Lex == "GoTo")
                {
                    return GoToNode(tokens);
                }
                if (tokens[0].TokenType == TokenType.Identifier && Functions.Contains(tokens[0].Lex))
                {
                    if (tokens.Length < 3 || tokens[1].Lex != "(" || tokens[^1].Lex != ")")
                    {
                        _ = new Exception(ExceptionType.LineOvercharge, tokens[0].Line + 1, "Delimiters expected");
                        return null;
                    }

                    GenericNode[] args = [];

                    if (tokens.Length > 3)
                    {
                        args = ProcessFunctionArgument(tokens[2..^1]); //ignoring function and delimiters for the tokens array
                    }

                    GenericFunction gn = BuildFunction(tokens[0].Lex, tokens[0].Line, args);
                    gn?.ValidateArgument();
                    return gn;
                }

                if (tokens[1].Lex == "<-")
                {
                    GenericNode an = BuildArgument(tokens[2..]);
                    if (an == null) { return new("", -1); }
                    if (an.GetType() != typeof(ArithmeticOperatorNode) && an.GetType() != typeof(Variable) && an.GetType() != typeof(GetColorCount) &&
                    an.GetType() != typeof(IsCanvasColor) && an.GetType() != typeof(NonArgumentReturn) && an.GetType() != typeof(OneIntArgumentReturn) &&
                    an.GetType() != typeof(OneStringArgumentReturn))
                    {
                        _ = new Exception(ExceptionType.Argument, tokens[0].Line + 1, "Invalid assignment");
                    }

                    return new AssignmentNode(tokens[0].Lex, tokens[0].Line, an);
                }
            }
            else if (tokens.Length != 0)
            {
                _ = new Exception(ExceptionType.Argument, tokens[0].Line + 1, "Delimiters expected");
            }

            _ = new Exception(ExceptionType.Argument, tokens[0].Line + 1, "Delimiters expected");

            return new("", -1);
        }

        private static GenericFunction BuildFunction(string lex, int line, GenericNode[] args)
        {
            return lex switch
            {
                //functtions
                "GetActualX" => new NonArgumentReturn(lex, line, FunctionType.GetActualX, [.. args]),
                "GetActualY" => new NonArgumentReturn(lex, line, FunctionType.GetActualY, [.. args]),
                "GetCanvasSize" => new NonArgumentReturn(lex, line, FunctionType.GetCanvasSize, [.. args]),
                "GetColorCount" => new GetColorCount(lex, line, FunctionType.GetColorCount, [.. args]),
                "IsBrushColor" => new OneStringArgumentReturn(lex, line, FunctionType.IsBrushColor, [.. args]),
                "IsBrushSize" => new OneIntArgumentReturn(lex, line, FunctionType.IsBrushSize, [.. args]),
                "IsCanvasColor" => new IsCanvasColor(lex, line, FunctionType.IsCanvasColor, [.. args]),
                //instructions
                "ReSpawn" => new TwoIntsArgument(lex, line, FunctionType.ReSpawn, [.. args]),
                "Color" => new OneStringArgument(lex, line, FunctionType.Color, [.. args]),
                "Size" => new OneIntArgument(lex, line, FunctionType.Size, [.. args]),
                "DrawLine" => new ThreeIntsArgument(lex, line, FunctionType.DrawLine, [.. args]),
                "DrawCircle" => new ThreeIntsArgument(lex, line, FunctionType.DrawCircle, [.. args]),
                "DrawRectangle" => new FiveIntsArgument(lex, line, FunctionType.DrawRectangle, [.. args]),
                "Fill" => new NonArgument(lex, line, FunctionType.Fill, [.. args]),
                "DrawPixel" => new TwoIntsArgument(lex, line, FunctionType.DrawPixel, [.. args]),
                _ => null,
            };
        }

        private static GoToJump GoToNode(Token[] tokens)
        {
            if (tokens.Length < 7)
            {
                _ = new Exception(ExceptionType.Argument, tokens[0].Line + 1, "Non complete defintion for a GoTo: \"GoTo [label] (boolean condition)\"");
                return null;
            }
            if (tokens[1].Lex != "[" || tokens[2].TokenType != TokenType.Identifier || tokens[3].Lex != "]")
            {
                _ = new Exception(ExceptionType.Argument, tokens[0].Line + 1, "Non valid label declaration \"GoTo [label]\"");
                return null;
            }
            if (tokens[4].Lex != "(" || tokens[^1].Lex != ")")
            {
                _ = new Exception(ExceptionType.Argument, tokens[0].Line + 1, "Delimiters expected for the boolean condition declaration");
                return null;
            }

            GenericBooleanNode condition = BuildBooleanNode(tokens[5..^1]);

            return new GoToJump(tokens[0].Lex, tokens[0].Line, condition, tokens[2].Lex);
        }
    }
}