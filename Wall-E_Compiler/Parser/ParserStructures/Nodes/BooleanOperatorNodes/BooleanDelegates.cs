namespace Parser
{
    abstract partial class GenericBooleanNode : GenericNode
    {
        public delegate bool BooleanOperation(bool x, bool y);
        public delegate bool BooleanComparison(int x, int y);
    }
}