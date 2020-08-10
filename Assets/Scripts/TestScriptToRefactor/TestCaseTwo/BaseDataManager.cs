namespace TestScriptToRefactor.TestCaseTwo
{
    public class BaseDataManager
    {
        public class ItemCardRecords
        {
            public static ItemInfo GetItem(int userItem)
            {
                return new ItemInfo();
            }
        }

        public class HallDataRecords
        {
            public class GameHallType
            {
                public static object All { get; set; }
            }

            public static int GetDenomByHallId(object all, int hallId)
            {
                return 0;
            }
        }
    }
}