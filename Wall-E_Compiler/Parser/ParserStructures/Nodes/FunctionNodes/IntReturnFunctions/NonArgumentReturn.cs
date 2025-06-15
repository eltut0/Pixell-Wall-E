using System;
using System.Collections.Generic;

namespace Parser
{
    class NonArgumentReturn(string lex, int line, FunctionType functionType, List<GenericNode> arguments) : GenericFunction(lex, line, functionType, arguments)
    {
        protected delegate int Operation();
        private readonly Operation _Operation = _operations[functionType];
        private static readonly Dictionary<FunctionType, Operation> _operations = new()
        {
            {FunctionType.GetCanvasSize, GetCanvasSize},
            {FunctionType.GetActualX, GetActualX},
            {FunctionType.GetActualY, GetActualY},
        };
        public override void ExecuteNode()
        {
            Result = _Operation();
        }
        static int GetCanvasSize()
        {
            return GlobalParameters.ProjectGlobalParameters.CanvasSize;
        }
        static int GetActualX()
        {
            return Compiler.CodeCompiler.XPosition;
        }
        static int GetActualY()
        {
            return Compiler.CodeCompiler.YPosition;
        }
    }
}