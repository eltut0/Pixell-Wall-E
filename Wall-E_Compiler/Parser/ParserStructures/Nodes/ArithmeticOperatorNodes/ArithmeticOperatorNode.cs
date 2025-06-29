using Godot;

namespace Parser
{
    partial class ArithmeticOperatorNode : GenericNode
    {
        public ArithmeticOperator Operation { get; set; }
        public ArithmeticOperatorNode(string lex, int Line, ArithmeticOperator arithmeticOperator, GenericNode firstChild,
        GenericNode secondChild) : base(lex, Line)
        {
            Operation = arithmeticOperator;
            AddChild(firstChild);
            AddChild(secondChild);
        }
        public override void ExecuteNode()
        {
            if (Children.Count == 0)
            {
                if (int.TryParse(Lex, out int Val))
                {
                    Result = Val;
                }
                else
                {
                    _ = new Exception(ExceptionType.Argument, Line, Lex);
                }
            }
            else
            {
                foreach (var child in Children)
                {
                    child.ExecuteNode();
                }
                Result = Operation(Children[0].Result, Children[1].Result, Line);
            }
        }
    }
}