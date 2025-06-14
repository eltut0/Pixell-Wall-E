using ParserLibrary;

namespace Parser
{
    class AssignmentNode(string varKey, int line, GenericNode varValue) : GenericNode(varKey, line)
    {
        public GenericNode VarValue { get; private set; } = varValue;
        public override void ExecuteNode()
        {
            VarValue.ExecuteNode();
            Variable.VariablesDic[Lex] = ((ArithmeticOperatorNode)VarValue).Result; //every possible input type contains a definition for Result
        }
    }
}