namespace Parser
{
    partial class ArithmeticOperatorNode : GenericNode
    {
        protected ArithmeticOperator Operation { get; set; }
        protected int Result { get; set; }
        public ArithmeticOperatorNode(string lex, int Line, ArithmeticOperator arithmeticOperator, ArithmeticOperatorNode firstChild,
        ArithmeticOperatorNode secondChild) : base(lex, Line)
        {
            Operation = arithmeticOperator;
            AddChild(firstChild);
            AddChild(secondChild);
        }
        protected override void ExecuteNode()
        {
            if (Children.Count == 0)
            {
                if (int.TryParse(Lex, out int Value))
                {
                    Result = Value;
                }
                else
                {
                    Exception.exceptionList.Add(new Exception(ExceptionType.Argument, Line, Lex));
                }
            }
            else
            {
                foreach (var child in Children)
                {
                    ((ArithmeticOperatorNode)child).ExecuteNode();
                }
                Result = Operation(((ArithmeticOperatorNode)Children[0]).Result, ((ArithmeticOperatorNode)Children[1]).Result);
            }
        }
    }
}