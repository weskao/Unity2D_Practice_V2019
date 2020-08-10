using System.Collections.Generic;

namespace TestScriptToRefactor.TestCaseTwo
{
    public class RankingModel
    {
        public List<RankInfo> rankingDatas;
        public RankingMode _CurrrntMode;

        public static RankingModel Instance
        {
            get
            {
                return new RankingModel();
            }
        }

        public int HallIndexToReverseHallId()
        {
            return 0;
        }
    }
}