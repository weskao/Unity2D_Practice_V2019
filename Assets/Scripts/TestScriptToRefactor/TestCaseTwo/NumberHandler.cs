namespace TestScriptToRefactor.TestCaseTwo
{
    public static class NumberHandler
    {
        public static string ToCredit(this long number)
        {
            return $"{number:N0}"; // 1,002,395,000
        }

        public static string ToLimitCredit(this long number)
        {
            return $"{number:C0}"; // "1,002,395,000" or "1.02 M"
        }
    }
}