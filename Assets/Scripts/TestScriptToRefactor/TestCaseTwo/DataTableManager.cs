namespace TestScriptToRefactor.TestCaseTwo
{
    public class DataTableManager
    {
        public static DataTableManager Instance
        {
            get
            {
                return new DataTableManager();
            }
        }

        public string GetText(string rankingMultiply)
        {
            return "MyString";
        }
    }
}