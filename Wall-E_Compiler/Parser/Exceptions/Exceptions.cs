using System.Collections.Generic;

namespace Parser
{
    class Exception
    {
        public static List<Exception> exceptionList = new List<Exception>();
        ExceptionType Type { get; set; }
        int Line { get; set; }
        string Lex { get; set; }
        public Exception(ExceptionType type, int line, string lex)
        {
            Type = type;
            Line = line;
            Lex = lex;
            exceptionList.Add(this);
        }
    }
}