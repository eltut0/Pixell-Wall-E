using System.Collections.Generic;

namespace Parser
{
    class GenericNode
    {
        protected string Lex { get; set; }
        protected int Line { get; set; }
        protected List<GenericNode> Children = new List<GenericNode>();
        public GenericNode(string lex, int line)
        {
            Lex = lex;
            Line = line;
            Children = new List<GenericNode>();
        }

        //define generic method for execute the node, override in the child classes
        protected virtual void ExecuteNode() { }


        protected void AddChild(GenericNode node)
        {
            Children.Add(node);
        }
    }
}