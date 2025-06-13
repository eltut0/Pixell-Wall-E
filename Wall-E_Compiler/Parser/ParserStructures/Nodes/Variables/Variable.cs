using System.Collections.Generic;

namespace Parser
{
    class Variable(string lex, int line) : GenericNode(lex, line)
    {
        public int Result { get; private set; }
        public static readonly Dictionary<string, int> VariablesDic = [];

        public override void ExecuteNode()
        {
            if (VariablesDic.TryGetValue(Lex, out int Value))
            {
                Result = Value;
            }
            else
            {
                _ = new Exception(ExceptionType.NotDefinedObject, Line, Lex);
            }
        }
    }
}