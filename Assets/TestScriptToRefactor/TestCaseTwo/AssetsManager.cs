namespace TestScriptToRefactor.TestCaseTwo
{
    public class AssetsManager
    {
        public static AssetsManager Instance
        {
            get
            {
                return new AssetsManager();
            }
        }
    }
}