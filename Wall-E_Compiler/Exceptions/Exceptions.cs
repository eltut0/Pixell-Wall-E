using System.Collections.Generic;

namespace Parser
{
    class Exception
    {
        public static List<Exception> exceptionList = [];
        public ExceptionType Type { get; set; }
        public int Line { get; set; }
        public string Lex { get; set; }
        public Exception(ExceptionType type, int line, string lex)
        {
            Type = type;
            Line = line;
            Lex = lex;
            exceptionList.Add(this);
        }
    }
}