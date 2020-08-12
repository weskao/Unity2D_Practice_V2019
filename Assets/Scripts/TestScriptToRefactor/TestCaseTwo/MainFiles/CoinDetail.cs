using System;
using TestScriptToRefactor.TestCaseOne;
using UnityEngine;
using UnityEngine.UI;

namespace TestScriptToRefactor.TestCaseTwo.MainFiles
{
    [Serializable]
    public class CoinDetail
    {
        public Button coinLengthSwitchButton;

        public Animator switchCoinLengthAnimator;

        public TextMeshProUGUI completeCoinText;

        public long MaxValueToShowCoinDetail
        {
            get
            {
                return 999999999;
            }
        }

        public void SetButtonVisibility(bool isVisible)
        {
            coinLengthSwitchButton.gameObject.SetActive(isVisible);
        }
    }
}