using System.Collections.Generic;

namespace Parser
{
    class GenericNode
    {
        string Lex { get; set; }
        List<GenericNode> Children { get; set; } = new List<GenericNode>();
        public GenericNode(string lex)
        {
            Lex = lex;
            Children = new List<GenericNode>();
        }
        void AddChild(GenericNode node)
        {
            Children.Add(node);
        }
    }
}