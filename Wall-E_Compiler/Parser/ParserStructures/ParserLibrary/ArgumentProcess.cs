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
                        sb.Append(args[i].Lex + " ");
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
            if (args.Length == 0) { _ = new Exception(ExceptionType.Argument, -1, "Missing element"); return null; }
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
            int index = -1;

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].Lex == "||")
                {
                    index = i;
                    break;
                }
            }

            if (index == -1)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    if (args[i].Lex == "&&")
                    {
                        index = i;
                        break;
                    }
                }
            }

            if (index == -1)
            {
                //return new comparison node
                return GenerateComparisonNode(args);
            }

            if (index != -1)
            {
                GenericBooleanNode.BooleanOperation operation = null;
                switch (args[index].Lex)
                {
                    case "&&":
                        operation = GenericBooleanNode.And;
                        break;
                    case "||":
                        operation = GenericBooleanNode.Or;
                        break;
                }

                if (index == 0 || index == args.Length - 1)
                {
                    _ = new Exception(ExceptionType.Argument, args[0].Line + 1, "Missing arithmetic operation or variable reference");
                }//this case will call another node with an empty tokens array

                GenericBooleanNode _left = BuildBooleanNode(args[..index]);
                GenericBooleanNode _right = BuildBooleanNode(args[(index + 1)..]);
                return new LogicalNode(args[0].Lex, args[0].Line, _left, _right, operation);
            }

            _ = new Exception(ExceptionType.Argument, args[0].Line + 1, "Error at the logical argument for the GoTo");
            return new LogicalNode("", -1, null, null, null);

        }

        private static ComparisonNode GenerateComparisonNode(Token[] args)
        {
            if (args.Any(x => BooleanComparators.Contains(x.Lex)))
            {
                for (int i = 0; i < args.Length; i++)
                {
                    if (BooleanComparators.Contains(args[i].Lex))
                    {
                        GenericNode _left = BuildArithmeticNode(args[..i]);
                        GenericNode _right = BuildArithmeticNode(args[(i + 1)..]);
                        GenericBooleanNode.BooleanComparison operation = null;
                        switch (args[i].Lex)
                        {
                            case ">":
                                operation = GenericBooleanNode.GreaterThan;
                                break;
                            case "<":
                                operation = GenericBooleanNode.LessThan;
                                break;
                            case ">=":
                                operation = GenericBooleanNode.GreaterOrEqual;
                                break;
                            case "<=":
                                operation = GenericBooleanNode.LessOrEqual;
                                break;
                            case "==":
                                operation = GenericBooleanNode.Equal;
                                break;
                        }

                        return new ComparisonNode(args[i].Lex, args[i].Line, _left, _right, operation);
                    }
                }
            }
            _ = new Exception(ExceptionType.TypeError, args[0].Line + 1, "Cannot apply a boolean operation to an arithmetic expression");
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