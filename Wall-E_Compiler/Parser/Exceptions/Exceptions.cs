using System.Collections.Generic;

namespace Parser
{
    class Exception
    {
        public static List<Exception> exceptionList = new List<Exception>();
        ExceptionType type { get; set; }
        int line { get; set; }
        string lex { get; set; }
        public Exception(ExceptionType type, int line, string lex)
        {
            this.type = type;
            this.line = line;
            this.lex = lex;
            exceptionList.Add(this);
        }
    }
}