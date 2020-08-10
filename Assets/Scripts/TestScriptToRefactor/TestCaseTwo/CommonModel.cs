namespace TestScriptToRefactor.TestCaseTwo
{
    public class CommonModel

    {
        public static CommonModel Instance
        {
            get
            {
                return new CommonModel();
            }
        }

        public string GetCreditText(long infoWin)
        {
            return "MyString";
        }
    }
}