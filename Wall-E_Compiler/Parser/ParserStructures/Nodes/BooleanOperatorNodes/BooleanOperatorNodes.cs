namespace Parser
{
    partial class BooleanOperatorNode : GenericNode
    {
        protected BooleanOperator Operation { get; set; }
        protected bool Result { get; set; }
        public BooleanOperatorNode(string lex, int line, BooleanOperator booleanOperator, BooleanOperatorNode firstChild,
        BooleanOperatorNode secondChild) : base(lex, line)
        {
            Operation = booleanOperator;
            AddChild(firstChild);
            AddChild(secondChild);
        }
        protected override void ExecuteNode()
        {

        }
    }
}