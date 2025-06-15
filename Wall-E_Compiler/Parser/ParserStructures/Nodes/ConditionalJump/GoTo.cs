namespace Parser
{
    public class GoToJump(string lex, int line, GenericBooleanNode condition, MyLabel label) : GenericNode(lex, line)
    {
        public readonly GenericBooleanNode Condition = condition;
        public readonly MyLabel Label = label;
        public bool ValidJump()
        {
            Condition.ExecuteNode();
            return Condition.Result;
        }
    }
}