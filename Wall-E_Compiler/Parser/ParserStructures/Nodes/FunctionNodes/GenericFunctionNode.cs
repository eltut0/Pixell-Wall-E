namespace Parser
{
    abstract class GenericFunction(string lex, int line) : GenericNode(lex, line)
    {
        public abstract void ValidateArgument();
    }
}