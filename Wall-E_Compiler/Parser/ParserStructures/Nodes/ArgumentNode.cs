namespace Parser
{
    class ArgumentNode : GenericNode
    {
        string Argument { get; set; }
        public ArgumentNode(string lex, string argument) : base(lex)
        {
            Argument = argument;
        }
    }
}