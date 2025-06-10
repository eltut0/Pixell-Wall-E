namespace Parser
{
    public class GoToJump(string lex, int line, GenericBooleanNode condition, MyLabel label) : GenericNode(lex, line)
    {
        private readonly GenericBooleanNode Condition = condition;
        private readonly MyLabel Label = label;
        public bool ValidJump()
        {
            Condition.ExecuteNode();
            return Condition.Result;
        }
    }
}