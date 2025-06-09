using System.Collections.Generic;

namespace Parser
{
    public abstract partial class GenericBooleanNode(string lex, int line) : GenericNode(lex, line)
    {
        public bool Result { get; set; }
        protected new List<GenericBooleanNode> Children = [];
    }
    class LogicalNode : GenericBooleanNode
    {
        public LogicalNode(string lex, int line, GenericBooleanNode left, GenericBooleanNode right, BooleanOperation operation) : base(lex, line)
        {
            AddChild(left);
            AddChild(right);
            Operation = operation;
        }
        public BooleanOperation Operation { get; }
        public override void ExecuteNode()
        {
            foreach (var child in Children)
            {
                child.ExecuteNode();
            }
            Result = Operation(Children[0].Result, Children[1].Result);
        }
    }
    class ComparisonNode : GenericBooleanNode
    {
        public ComparisonNode(string lex, int line, ElementNode left, ElementNode right, BooleanComparison operation) : base(lex, line)
        {
            AddChild(left);
            AddChild(right);
            Operation = operation;
        }
        public BooleanComparison Operation { get; }
        public override void ExecuteNode()
        {
            Result = Operation(((ElementNode)Children[0]).Value, ((ElementNode)Children[1]).Value);
        }
    }
    class ElementNode(string lex, int line, int value) : GenericBooleanNode(lex, line)
    {
        public int Value { get; } = value;

        public override void ExecuteNode() { }
    }
}