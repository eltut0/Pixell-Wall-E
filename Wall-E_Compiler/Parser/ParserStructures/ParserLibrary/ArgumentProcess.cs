using Godot;
using Lexer;
using Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Exception = Parser.Exception;

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
            if (args.Length >= 3)
            {
                if (args[0].Lex == "\"" && args[^1].Lex == "\"")
                {
                    StringBuilder sb = new();
                    for (int i = 1; i < args.Length - 1; i++)
                    {
                        sb.Append(args[i].Lex);
                    }
                    GenericNode gn = new(sb.ToString(), args[0].Line)
                    {
                        IsString = true
                    };
                    return gn;
                }
            }

            return BuildArithmeticNode(args);
        }

        private static GenericNode BuildArithmeticNode(Token[] args)
        {
            if (args.Length == 0)
            {
                _ = new Exception(ExceptionType.Argument, -1, "Missing element");
                return null;
            }

            if (args.Any(c => c.Lex == "**" || c.Lex == "%" || c.Lex == "*" || c.Lex == "/" || c.Lex == "+" || c.Lex == "-"))
            {
                int operatorPosition = -1;


                for (int i = args.Length - 1; i >= 0; i--)
                {
                    if (args[i].Lex == "**" && !IsInsideParenthesis(args[i], args))
                    {
                        operatorPosition = i;
                    }
                }

                if (operatorPosition == -1)
                {
                    for (int i = 0; i < args.Length; i++)
                    {
                        if ((args[i].Lex == "*" || args[i].Lex == "/" || args[i].Lex == "%") && !IsInsideParenthesis(args[i], args))
                        {
                            operatorPosition = i;
                        }
                    }
                }
                if (operatorPosition == -1)
                {
                    for (int i = 0; i < args.Length; i++)
                    {
                        if ((args[i].Lex == "+" || args[i].Lex == "-") && !IsInsideParenthesis(args[i], args))
                        {
                            operatorPosition = i;
                        }
                    }
                }

                if (operatorPosition != -1)
                {
                    ArithmeticOperatorNode.ArithmeticOperator operation = null;
                    switch (args[operatorPosition].Lex)
                    {
                        case "**":
                            operation = ArithmeticOperatorNode.Pow;
                            break;
                        case "*":
                            operation = ArithmeticOperatorNode.Multiply;
                            break;
                        case "/":
                            operation = ArithmeticOperatorNode.Divide;
                            break;
                        case "+":
                            operation = ArithmeticOperatorNode.Sum;
                            break;
                        case "-":
                            operation = ArithmeticOperatorNode.Subtract;
                            break;
                        case "%":
                            operation = ArithmeticOperatorNode.Modulo;
                            break;
                    }

                    if (operatorPosition == 0 || operatorPosition == args.Length - 1)
                    {
                        _ = new Exception(ExceptionType.Argument, args[0].Line + 1, "Missing arithmetic operation or variable reference");
                    }//this case will call another node with an empty tokens array

                    GenericNode _left = BuildArgument(args[..operatorPosition]);
                    GenericNode _right = BuildArgument(args[(operatorPosition + 1)..]);
                    return new ArithmeticOperatorNode(args[0].Lex, args[0].Line, operation, _left, _right);
                }

            }

            if (args.Length == 1)
            {
                if (IsInteger(args[0].Lex))
                {
                    ArithmeticOperatorNode temp = new(args[0].Lex, args[0].Line, null, null, null);
                    temp.Children.Clear(); //must clear childs list
                    return temp;
                }

                return new Variable(args[0].Lex, args[0].Line);
            }

            //check for a function
            if (ReturnFunctions.Contains(args[0].Lex))
            {
                if (args.Length >= 3)
                {
                    if (args[1].Lex == "(" && args[^1].Lex == ")")
                    {
                        return ConvertLineToTree(args); //recursively call and will make a function node
                    }
                }
                _ = new Exception(ExceptionType.SyntaxError, args[0].Line + 1, "Delimiters expected");
            }

            _ = new Exception(ExceptionType.Argument, args[0].Line + 1, "Unexpected argument");
            return new GenericNode("", args[0].Line);
        }

        private static GenericBooleanNode BuildBooleanNode(Token[] args)
        {
            int opIndex = FindOperator(args, "||");
            if (opIndex == -1)
            {
                opIndex = FindOperator(args, "&&");
            }

            if (opIndex != -1)
            {
                if (opIndex == 0 || opIndex == args.Length - 1)
                {
                    _ = new Exception(ExceptionType.Argument, args[opIndex].Line,
                        $"Missing operands for operator '{args[opIndex].Lex}'");
                    return null;
                }

                var leftNode = BuildBooleanNode(args[..opIndex]);
                var rightNode = BuildBooleanNode(args[(opIndex + 1)..]);

                if (leftNode == null || rightNode == null)
                {
                    return null;
                }

                GenericBooleanNode.BooleanOperation operation = args[opIndex].Lex == "&&"
                    ? GenericBooleanNode.And
                    : GenericBooleanNode.Or;

                return new LogicalNode(args[opIndex].Lex, args[opIndex].Line, leftNode, rightNode, operation);
            }

            return GenerateComparisonNode(args);
        }

        private static int FindOperator(Token[] args, string op)
        {
            int depth = 0;
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].Lex == "(") depth++;
                else if (args[i].Lex == ")") depth--;
                else if (depth == 0 && args[i].Lex == op) return i;
            }
            return -1;
        }

        private static ComparisonNode GenerateComparisonNode(Token[] args)
        {
            foreach (var token in args)
            {
                if (BooleanComparators.Contains(token.Lex))
                {
                    int opIndex = Array.IndexOf(args, token);

                    if (opIndex == 0 || opIndex == args.Length - 1)
                    {
                        _ = new Exception(ExceptionType.Argument, token.Line,
                            $"Missing operands for comparison operator '{token.Lex}'");
                        return null;
                    }

                    var leftNode = BuildArithmeticNode(args[..opIndex]);
                    var rightNode = BuildArithmeticNode(args[(opIndex + 1)..]);

                    if (leftNode == null || rightNode == null)
                    {
                        _ = new Exception(ExceptionType.Argument, token.Line,
                            "Invalid operands for comparison");
                        return null;
                    }

                    GenericBooleanNode.BooleanComparison operation = token.Lex switch
                    {
                        ">" => GenericBooleanNode.GreaterThan,
                        "<" => GenericBooleanNode.LessThan,
                        ">=" => GenericBooleanNode.GreaterOrEqual,
                        "<=" => GenericBooleanNode.LessOrEqual,
                        "==" => GenericBooleanNode.Equal,
                        _ => null
                    };

                    return new ComparisonNode(token.Lex, token.Line, leftNode, rightNode, operation);
                }
            }

            _ = new Exception(ExceptionType.TypeError, args[0].Line,
                "No valid comparison operator found");
            return null;
        }

        private static bool IsInteger(string x)
        {
            return int.TryParse(x, out int n);
        }

        private static bool IsInsideParenthesis(Token element, Token[] argument)
        {
            int balance = 0;
            foreach (var token in argument)
            {
                if (token.Lex == "(")
                {
                    balance++;
                }
                else if (token.Lex == ")")
                {
                    balance--;
                }
                if (token == element)
                {
                    return balance > 0;
                }
            }
            return false;
        }
    }
}