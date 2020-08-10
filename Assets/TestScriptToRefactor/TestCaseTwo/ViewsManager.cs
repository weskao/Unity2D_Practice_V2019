namespace TestScriptToRefactor.TestCaseTwo
{
    public class ViewsManager
    {
        public static ViewsManager Instance
        {
            get
            {
                return new ViewsManager();
            }
        }

        public void HideAndPush<T>(object instance)
        {
        }
    }
}