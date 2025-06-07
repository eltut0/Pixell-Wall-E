namespace Parser
{
    class AssignmentNode(string varKey, int line, ArithmeticOperatorNode varValue) : GenericNode(varKey, line)
    {
        public ArithmeticOperatorNode VarValue { get; private set; } = varValue;
        public override void ExecuteNode()
        {
            VarValue.ExecuteNode();
            _ = new Variable(Lex, Line, VarValue.Result);
        }
    }
}