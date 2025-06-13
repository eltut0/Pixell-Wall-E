using System.Collections.Generic;

namespace Parser
{
    public class GenericNode(string lex, int line)
    {
        public string Lex { get; set; } = lex;
        public int Line { get; set; } = line;
        public bool IsString { get; set; }
        public List<GenericNode> Children = [];

        public virtual void ExecuteNode() { }


        public void AddChild(GenericNode node)
        {
            if (node != null)
            {
                Children.Add(node);
            }
        }
    }
}