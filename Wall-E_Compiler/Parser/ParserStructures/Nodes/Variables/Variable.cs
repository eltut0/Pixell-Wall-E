using System.Collections.Generic;
using Godot;

namespace Parser
{
    class Variable(string lex, int line) : GenericNode(lex, line)
    {
        public static readonly Dictionary<string, int> VariablesDic = [];

        public override void ExecuteNode()
        {
            if (VariablesDic.TryGetValue(Lex, out int Value))
            {
                Result = Value;
            }
            else
            {
                _ = new Exception(ExceptionType.NotDefinedObject, Line + 1, Lex);
            }
        }
    }
}