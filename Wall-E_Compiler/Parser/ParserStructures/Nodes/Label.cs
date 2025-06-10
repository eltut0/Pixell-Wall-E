using System.Collections.Generic;

namespace Parser
{
    public class MyLabel : GenericNode
    {
        public static List<MyLabel> Labels = [];
        public MyLabel(string lex, int line) : base(lex, line)
        {
            Labels.Add(this);
        }
    }
}