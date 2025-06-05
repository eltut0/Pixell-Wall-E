using System;

namespace Parser
{
    partial class StringArgumentFunction : GenericFunction
    {
        public StringArgumentFunction(string lex, int line, string argument, StringArgumentOperation operation) : base(lex, line)
        {
            Argument = argument;
            Operation = operation;
        }
        private string Argument { get; }
        private StringArgumentOperation Operation { get; }
        public override void ValidateArgument()
        {
            throw new NotImplementedException();
        }
        protected override void ExecuteNode()
        {
            Operation(Argument);
        }
    }
}