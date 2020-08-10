using System;

namespace TestScriptToRefactor.TestCaseTwo
{
    public static class NumberHandler
    {
        public static string ToCredit(this long number)
        {
            return string.Format("{0:N0}", number); // 1,002,395,000
        }

        public static string ToLimitCredit(this long number)
        {
            return string.Format("{0:C0}", number); // "1,002,395,000" or "1.02 M"
        }
    }
}