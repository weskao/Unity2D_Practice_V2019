using System;
using System.Collections.Generic;
using TestScriptToRefactor.TestCaseOne;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TestScriptToRefactor.TestCaseTwo
{
    [Serializable]
    public class CoinDetail
    {
        public Button coinLengthSwitchButton;

        public Animator switchCoinLengthAnimator;

        public TextMeshProUGUI completeCoinText;

        public long MaxValueToShowCoinDetail => 999999999;

        public void SetButtonVisibility(bool isVisible)
        {
            coinLengthSwitchButton.gameObject.SetActive(isVisible);
        }
    }

    public class BaseRankingInfoScrollItem : ScrollItem, IPointerClickHandler
    {
        private static readonly int DetailHash = Animator.StringToHash("Detail");

        [SerializeField]
        protected PlayerPictureComponent _userPic = null;

        [SerializeField]
        protected TextMeshProUGUI _tmpRank = null;

        [SerializeField]
        protected Text _txtNickName = null;

        [SerializeField]
        protected TextMeshProUGUI _tmpWinCoin = null;

        [SerializeField]
        protected TextMeshProUGUI _tmpMutiply = null;

        [SerializeField]
        protected Image _imgCrown = null;

        [SerializeField]
        protected Image _imgGameIcon = null;

        [SerializeField]
        protected TextMeshProUGUI _tmpMachineNum = null;

        [SerializeField]
        protected TextMeshProUGUI _tmpActive = null;

        [SerializeField]
        protected GameReplayStateComponent _gameReplay = null;

        [SerializeField]
        protected Image _usedCard = null;

        [SerializeField]
        private GameObject _jpIcon;

        private string CROWN_IMAGE_PATH = UiPathDefine.RankingCrownImage;
        private string GAME_ICON_PATH = UiPathDefine.RankingGameBanner;

        protected RankInfo _rankInfo = null;
        protected string _memberId = string.Empty;

        [SerializeField]
        private CoinDetail _coinDetail;

        private void Awake()
        {
        }

        public void SwitchNumWinDisplay()
        {
            ShowHideCompleteValue();
        }

        private bool HasCoinDetailFeature()
        {
            return _tmpWinCoin != null &&
                   _rankInfo != null &&
                   _coinDetail.coinLengthSwitchButton != null &&
                   _coinDetail.switchCoinLengthAnimator != null &&
                   _coinDetail.completeCoinText != null &&
                   _rankInfo.win > _coinDetail.MaxValueToShowCoinDetail;
        }

        private void ShowHideCompleteValue()
        {
            _coinDetail.switchCoinLengthAnimator.SetBool(DetailHash, !_coinDetail.switchCoinLengthAnimator.GetBool(DetailHash));
        }

        public void UpdateItem(int idx)
        {
            var list = RankingModel.Instance.rankingDatas;

            if (idx < 0 || idx >= list.Count)
                return;

            RankInfo info = list[idx];

            SetData(info.memberID, list[idx]);

            var winNumber = _rankInfo.win;
            var completeNum = winNumber.ToCredit(); // 1,002,395,000
            var shortNum = winNumber.ToLimitCredit(); // "1,002,395,000" or "1.02 M"

            if (HasCoinDetailFeature())
            {
                // show short number
                _tmpWinCoin.text = shortNum;

                // SetCoinDetailText
                _coinDetail.completeCoinText.text = completeNum;

                // show button
                // _coinDetail.SetButtonVisibility(true); // this line is redundant
            }
            else
            {
                // show complete number
                _tmpWinCoin.text = completeNum;

                // Hide button
                _coinDetail.SetButtonVisibility(false);
            }
        }

        public virtual void SetData(string memberId, RankInfo info)
        {
            _rankInfo = info;
            _memberId = memberId;

            SetMemberInfo();

            if (null == info)
            {
                SetEmpty();
                return;
            }

            _tmpRank.gameObject.SetActive(info.rank > 3);
            _tmpRank.text = info.rank.ToString();

            if (RankingModel.Instance._CurrrntMode == RankingMode.ActivityMode)
            {
                _tmpWinCoin.text = info.win.ToString();
            }
            else
            {
                //int hallIdx = RankingModel.Instance.nowHallIdx;
                int hallId = RankingModel.Instance.HallIndexToReverseHallId();
                //int denom = MachineModel.Instance.GetDenomByHallId( hallId );
                int denom = BaseDataManager.HallDataRecords.GetDenomByHallId(BaseDataManager.HallDataRecords.GameHallType.All, hallId);

                if (null != _tmpWinCoin)
                {
                    _tmpWinCoin.text = CommonModel.Instance.GetCreditText(info.win);
                    /*
                    if ( RankingModel.Instance.nowCategory == RankingType.Rich ) {
                        _tmpWinCoin.text = info.win.CentToCredit();
                    }
                    else {
                        _tmpWinCoin.text = CommonModel.Instance.GetCreditTextByDenomNoDecimal( info.win, denom );
                    }
                    */
                }

                if (null != _tmpMutiply)
                {
                    _tmpMutiply.text = string.Format(DataTableManager.Instance.GetText("RANKING_MULTIPLY"), info.mag);
                }

                if (null != _tmpMachineNum)
                {
                    _tmpMachineNum.text = string.Empty;
                }
            }

            SetCard(info.useItemList);
            SetCrown(info.rank);
            SetGameState();
            SetJpIcon();
        }

        public class MyMemberInfo
        {
            public string nickName;
            public object memberID;
        }

        protected void SetMemberInfo()
        {
            if (IsPersonal(_memberId))
            {
                var memberInfo = UserDataManager.Profile.memberInfo;
                _userPic.LoadPicture(_memberId, PlayerInfoModel.PhotoSize.Small);

                _txtNickName.text = memberInfo.nickName;

                //if (null != _tmpActive)
                //{
                //    _tmpActive.text = string.Format( DataTableManager.Instance.GetText( "RANKING_ACTIVE" ), memberInfo.activeValue );
                //}
            }
            else
            {
                if (null != _rankInfo)
                {
                    _txtNickName.text = _rankInfo.nickName;
                    _userPic.LoadPicture(_rankInfo.memberID, PlayerInfoModel.PhotoSize.Small);

                    //               if (null != _tmpActive)
                    //               {
                    //	_tmpActive.text = string.Format( DataTableManager.Instance.GetText( "RANKING_ACTIVE" ), _rankInfo.activeValue );
                    //}
                }
            }
        }

        protected void SetEmpty()
        {
            _tmpRank.gameObject.SetActive(true);
            _tmpRank.text = "- -";

            _imgCrown.gameObject.SetActive(false);

            if (null != _tmpWinCoin)
                _tmpWinCoin.text = "- - -";

            if (null != _tmpMutiply)
                _tmpMutiply.text = "- - -";

            if (null != _tmpMachineNum)
                _tmpMachineNum.text = "- - -";

            if (null != _imgGameIcon)
                // _imgGameIcon.gameObject.SetActive(false);

                if (null != _gameReplay)
                {
                    // _gameReplay.gameObject.SetActive(false);
                }
        }

        private bool IsPersonal(string memberId)
        {
            return memberId.Equals(UserDataManager.Profile.memberInfo.memberID);
        }

        protected void SetCrown(int rank)
        {
            if (rank >= 1 && rank <= 3)
            {
                string path = string.Format(CROWN_IMAGE_PATH, rank);
                _imgCrown.sprite = new Sprite();
                _imgCrown.SetNativeSize();
                _imgCrown.gameObject.SetActive(true);
            }
            else
            {
                _imgCrown.gameObject.SetActive(false);
            }
        }

        protected void Downloading()
        {
            // _gameReplay.UpdateState();
        }

        protected void DownloadComplete()
        {
            // _gameReplay.UpdateState();
        }

        protected void SetGameState()
        {
            // string iconPath = string.Format(GAME_ICON_PATH, _rankInfo.scgGameId);

            if (null != _imgGameIcon)
            {
                _imgGameIcon.gameObject.SetActive(true);
                // _imgGameIcon.sprite = AssetsManager.Instance.LoadAsset<Sprite>(iconPath);
                _imgGameIcon.SetNativeSize();
            }

            if (null != _gameReplay)
            {
                // _gameReplay.gameObject.SetActive(true);
                // _gameReplay.SetData(_rankInfo.scgGameId, _rankInfo.historySn, _rankInfo.memberID);
            }
        }

        protected void SetJpIcon()
        {
            if (null != _rankInfo)
            {
                if (_jpIcon != null)
                {
                    // _jpIcon.SetActive(_rankInfo.HaveJp());
                }
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!eventData.pointerEnter.GetInstanceID().Equals(this.gameObject.GetInstanceID()))
                return;

            if (null != _rankInfo)
            {
                if (!PlayerInfoModel.GetInstance().Open(_rankInfo.memberID)) return;
                ViewsManager.Instance.HideAndPush<RankingView>(RankingModel.Instance);
            }
        }

        protected void SetCard(List<int> userItemList)
        {
            if (null != _usedCard)
            {
                _usedCard.gameObject.SetActive(false);

                if (null == userItemList || userItemList.Count == 0)
                    return;

                ItemInfo itemInfo = BaseDataManager.ItemCardRecords.GetItem(userItemList[0]);

                if (null != itemInfo)
                {
                    string levelColor = ((ItemRare)itemInfo.itemRare).ToString();

                    _usedCard.gameObject.SetActive(true);
                    // _usedCard.sprite = AssetsManager.Instance.LoadAsset<Sprite>(string.Format(UiPathDefine.RankingCard, levelcolor));
                    _usedCard.sprite = new Sprite();
                }
            }
        }
    }
}