using System;
using System.Collections.Generic;

namespace Parser
{
    class NonArgumentReturn(string lex, int line, FunctionType functionType, List<GenericNode> arguments) : GenericFunction(lex, line, functionType, arguments)
    {
        public new int Result { get; private set; }
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
            throw new NotImplementedException();
        }
        static int GetActualX()
        {
            throw new NotImplementedException();
        }
        static int GetActualY()
        {
            throw new NotImplementedException();
        }
    }
}