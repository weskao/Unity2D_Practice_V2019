namespace TestScriptToRefactor.TestCaseTwo
{
    public class PlayerInfoModel
    {
        public static PlayerInfoModel GetInstance()
        {
            return new PlayerInfoModel();
        }

        public bool Open(object memberId)
        {
            return false;
        }

        public class PhotoSize
        {
            public static object Small { get; set; }
        }
    }
}