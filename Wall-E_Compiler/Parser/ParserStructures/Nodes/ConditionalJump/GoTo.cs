using System.Linq;
using Godot;

namespace Parser
{
    public class GoToJump(string lex, int line, GenericBooleanNode condition, string label) : GenericNode(lex, line)
    {
        public readonly GenericBooleanNode Condition = condition;
        public readonly string Label = label;
        public bool ValidJump()
        {
            Condition.ExecuteNode();
            Parser.PrintTree(Condition);
            GD.Print(Condition.Result);
            return Condition.Result;
        }
    }
}