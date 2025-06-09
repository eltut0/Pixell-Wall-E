using System.Collections.Generic;

namespace Parser
{
    public class Label : GenericNode
    {
        public static List<Label> Labels = [];
        public Label(string lex, int line) : base(lex, line)
        {
            Labels.Add(new Label(lex, line));
        }
    }
}