using System.Collections.Generic;

namespace Parser
{
    class NonArgument(string lex, int line, FunctionType functionType, List<GenericNode> arguments) : GenericFunction(lex, line, functionType, arguments)
    {
        protected delegate void Operation();
        private readonly Operation _Operation = _operations[functionType];
        private static readonly Dictionary<FunctionType, Operation> _operations = new()
        {
            {FunctionType.Fill, Fill},
        };
        static void Fill()
        {
            //implement
        }
    }
}