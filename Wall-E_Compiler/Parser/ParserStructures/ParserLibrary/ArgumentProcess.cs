using Lexer;
using Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
                    return new GenericNode(sb.ToString(), args[0].Line);
                }
            }

            return null;
        }

        private static GenericNode BuildArithmeticNode(Token[] args)
        {
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
                        _ = new Exception(ExceptionType.Argument, args[0].Line, "Missing arithmetic operation or variable reference");
                    }//this case will call another node with an empty tokens array

                    GenericNode _left = BuildArgument(args[..operatorPosition]);
                    GenericNode _right = BuildArgument(args[(operatorPosition + 1)..]);
                    return new ArithmeticOperatorNode(args[0].Lex, args[0].Line, operation, (ArithmeticOperatorNode)_left, (ArithmeticOperatorNode)_right);
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
                _ = new Exception(ExceptionType.SyntaxError, args[0].Line, "Delimiters expected");
            }

            _ = new Exception(ExceptionType.Argument, args[0].Line, "Unexpected argument");
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