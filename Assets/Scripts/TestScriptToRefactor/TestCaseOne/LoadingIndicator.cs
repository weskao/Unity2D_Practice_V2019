namespace TestScriptToRefactor.TestCaseOne
{
    public class LoadingIndicator
    {
        public static LoadingIndicator Instance
        {
            get
            {
                return new LoadingIndicator();
            }
        }

        public void Show(bool useBackground, bool indicator)
        {
        }

        public void Close()
        {
        }
    }
}