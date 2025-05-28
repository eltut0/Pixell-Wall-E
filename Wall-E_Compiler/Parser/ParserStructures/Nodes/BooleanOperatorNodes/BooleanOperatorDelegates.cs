using System;

namespace Parser
{
    partial class BooleanOperatorNode : GenericNode
    {
        public delegate bool BooleanOperator(string x, string y);

        bool Greater(string x, string y)
        {
            try
            {
                int firstArgument = int.Parse(x);
                int secondArgument = int.Parse(y);
                if (firstArgument > secondArgument)
                {
                    return true;
                }
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }
        bool Less(string x, string y)
        {
            try
            {
                int firstArgument = int.Parse(x);
                int secondArgument = int.Parse(y);
                if (firstArgument < secondArgument)
                {
                    return true;
                }
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }
        bool Equal(string x, string y)
        {
            if (x == y)
            {
                return true;
            }
            return false;
        }
        bool GreaterOrEqual(string x, string y)
        {
            try
            {
                int firstArgument = int.Parse(x);
                int secondArgument = int.Parse(y);
                if (firstArgument < secondArgument)
                {
                    return true;
                }
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }
        bool LessOrEqual(string x, string y)
        {

        }
    }
}