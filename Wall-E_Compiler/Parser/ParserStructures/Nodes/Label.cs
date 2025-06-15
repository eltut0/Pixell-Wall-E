using System.Collections.Generic;

namespace Parser
{
    public class MyLabel(string lex, int line) : GenericNode(lex, line)
    {
        public static List<MyLabel> Labels = [];
        public void AddList()
        {
            Labels.Add(this);
        }
    }
}