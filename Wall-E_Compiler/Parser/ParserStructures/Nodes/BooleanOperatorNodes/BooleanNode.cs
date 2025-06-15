using System.Collections.Generic;
using Godot;

namespace Parser
{
    public abstract partial class GenericBooleanNode(string lex, int line) : GenericNode(lex, line)
    {
        public new bool Result { get; protected set; }
        public new List<GenericBooleanNode> Children { get; } = [];

        public void AddChild(GenericBooleanNode child)
        {
            Children.Add(child);
        }
    }

    class LogicalNode : GenericBooleanNode
    {
        public BooleanOperation Operation { get; }

        public LogicalNode(string lex, int line, GenericBooleanNode left, GenericBooleanNode right, BooleanOperation operation)
            : base(lex, line)
        {
            AddChild(left);
            AddChild(right);
            Operation = operation;
        }

        public override void ExecuteNode()
        {
            if (Children.Count != 2)
            {
                _ = new Exception(ExceptionType.Argument, Line + 1, "LogicalNode must have exactly 2 children");
                Result = false;
                return;
            }

            Children[0].ExecuteNode();
            Children[1].ExecuteNode();

            Result = Operation(Children[0].Result, Children[1].Result);
        }
    }

    class ComparisonNode(string lex, int line, GenericNode left, GenericNode right, GenericBooleanNode.BooleanComparison operation) : GenericBooleanNode(lex, line)
    {
        public GenericNode Left { get; } = left;
        public GenericNode Right { get; } = right;
        public BooleanComparison Operation { get; } = operation;

        public override void ExecuteNode()
        {
            Left.ExecuteNode();
            Right.ExecuteNode();

            Result = Operation(Left.Result, Right.Result);
        }
    }
}