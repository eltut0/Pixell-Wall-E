using System.Collections.Generic;

namespace Parser
{
    class GenericNode(string lex, int line)
    {
        public string Lex { get; set; } = lex;
        public int Line { get; set; } = line;
        protected List<GenericNode> Children = new List<GenericNode>();

        public virtual void ExecuteNode() { }


        protected void AddChild(GenericNode node)
        {
            if (node != null)
            {
                Children.Add(node);
            }
        }
    }
}