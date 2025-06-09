namespace Parser
{
    public class GoToJump(string lex, int line, GenericBooleanNode condition, Label label) : GenericNode(lex, line)
    {
        private readonly GenericBooleanNode Condition = condition;
        private readonly Label Label = label;
        public bool ValidJump()
        {
            Condition.ExecuteNode();
            return Condition.Result;
        }
    }
}