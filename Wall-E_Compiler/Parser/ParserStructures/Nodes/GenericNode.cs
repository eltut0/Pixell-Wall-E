using System.Collections.Generic;

namespace Parser
{
    class GenericNode(string lex, int line)
    {
        public string Lex { get; set; } = lex;
        public int Line { get; set; } = line;
        protected List<GenericNode> Children = new List<GenericNode>();

        //define generic method for execute the node, override in the child classes
        protected virtual void ExecuteNode() { }


        protected void AddChild(GenericNode node)
        {
            Children.Add(node);
        }
    }
}