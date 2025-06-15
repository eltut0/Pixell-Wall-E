using System;
using System.Collections.Generic;
using Compiler;
using Godot;

namespace Parser
{
    class TwoIntsArgument(string lex, int line, FunctionType functionType, List<GenericNode> arguments) : GenericFunction(lex, line, functionType, arguments)
    {
        private delegate void Operation(int x, int y);
        private readonly Operation _operation = _operations[functionType];
        protected override void SpecialValidation()
        {
            if (Children.Count == 0 || Children == null)
            {
                _ = new Exception(ExceptionType.Argument, Line, $"Non valid argument");
                return;
            }
            foreach (var arg in Children)
            {
                if (!(arg.GetType() == typeof(Variable) || arg.GetType() == typeof(ArithmeticOperatorNode)))
                {
                    _ = new Exception(ExceptionType.Argument, Line, $"Non valid argument");
                    return;
                }
            }
        }
        public override void ExecuteNode()
        {
            foreach (var arg in Children)
            {
                arg.ExecuteNode();
            }
            _operation(Children[0].Result, Children[1].Result);
        }
        private static readonly Dictionary<FunctionType, Operation> _operations = new()
        {
            {FunctionType.Spawn, Spawn},
            {FunctionType.ReSpawn, Spawn},
            { FunctionType.DrawPixel, DrawPixel},
        };

        static void Spawn(int x, int y)
        {
            if (x >= 0 && x < GlobalParameters.ProjectGlobalParameters.CanvasSize && y >= 0 && y < GlobalParameters.ProjectGlobalParameters.CanvasSize)
            {
                CodeCompiler.XPosition = x;
                CodeCompiler.YPosition = y;
                return;
            }

            _ = new Exception(ExceptionType.Argument, 1, "Invalid spawn coordinates");
        }
        static void DrawPixel(int x, int y)
        {
            if (x >= 0 && x < GlobalParameters.ProjectGlobalParameters.CanvasSize && y >= 0 && y < GlobalParameters.ProjectGlobalParameters.CanvasSize)
            {
                CodeCompiler.XPosition = x;
                CodeCompiler.YPosition = y;
                FunctionAuxMethods.DrawPixel();
                return;
            }
            _ = new Exception(ExceptionType.Argument, 1, "Invalid draw coordinates");
        }

    }
}