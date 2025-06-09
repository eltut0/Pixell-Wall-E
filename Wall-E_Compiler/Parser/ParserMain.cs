namespace Parser
{
    public class Parser
    {
        public static void ParserInit()
        {
            //clear previous data
            Exception.exceptionList.Clear();
            Variable.VariablesDic.Clear();
            Label.Labels.Clear();
        }
    }
}