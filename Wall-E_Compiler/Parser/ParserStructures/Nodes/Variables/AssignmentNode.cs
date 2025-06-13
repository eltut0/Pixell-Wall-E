namespace Parser
{
    class AssignmentNode(string varKey, int line, ArithmeticOperatorNode varValue) : GenericNode(varKey, line)
    {
        public ArithmeticOperatorNode VarValue { get; private set; } = varValue;
        public override void ExecuteNode()
        {
            VarValue.ExecuteNode();
            Variable.VariablesDic[Lex] = VarValue.Result;
        }
    }
}