using System.Collections.Generic;

namespace Parser
{
    class Variable : GenericNode
    {
        public int Result { get; private set; }
        public static readonly Dictionary<string, int> VariablesDic = [];
        public Variable(string lex, int line, int value) : base(lex, line)
        {
            VariablesDic[lex] = value;
        }

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