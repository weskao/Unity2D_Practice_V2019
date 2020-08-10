using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TestScriptToRefactor.TestCaseOne
{
    public class ActivityBuyProductActionBase : MonoBehaviour
    {
        #region Unimportant Code

        [SerializeField]
        protected ProductAction[] _actions = null;

        //目前ios不得出現discountSign, 故作此暫時方案
        [SerializeField]
        private GameObject discountSign = null;

        protected Button myButton;

        private string GetFinalProductId(ProductInfo[] products)
        {
            return "";
        }

        public List<string> Products
        {
            get
            {
                int vip = 5;
                List<string> productList = new List<string>();

                for (int i = 0; i < _actions.Length; ++i)
                {
                    productList.Add(
                        GetFinalProductId(_actions[i].products)
                    );
                }

                return productList;
            }
        }

        private static int _fakeVIPLevel = 0;
        private bool _isOneClickUpgradeLevelEnabled = false;

        // TODO: Refactor _hasAnyProduct 相關的 Code
        private bool _hasAnyProduct = false;

        public ActivityBuyProductActionBase(Button myButton)
        {
            this.myButton = myButton;
        }

        protected virtual void OnBuyBtnClick(ProductAction action)
        {
            string productId = GetFinalProductId(action.products);
            ShopModel.Instance.BuyProductID(productId, action.button.gameObject);
        }

        private void FillValueToView(ProductAction actionItem, decimal price, long oriValue, long promoteValue, int vipPoint)
        {
            if (actionItem == null)
                return;

            if (actionItem.valueGroup.tmp_price != null)
                actionItem.valueGroup.tmp_price.text = "$" + price;
            if (actionItem.valueGroup.txt_price != null)
                actionItem.valueGroup.txt_price.text = "$" + price;

            if (actionItem.valueGroup.tmp_oriValue != null)
                actionItem.valueGroup.tmp_oriValue.text = oriValue.ToString();
            if (actionItem.valueGroup.txt_oriValue != null)
                actionItem.valueGroup.txt_oriValue.text = oriValue.ToString();

            if (actionItem.valueGroup.tmp_promoteValue != null)
                actionItem.valueGroup.tmp_promoteValue.text = promoteValue.ToString();
            if (actionItem.valueGroup.txt_promoteValue != null)
                actionItem.valueGroup.txt_promoteValue.text = promoteValue.ToString();

            if (actionItem.valueGroup.tmp_vipPoint != null)
                actionItem.valueGroup.tmp_vipPoint.text = string.Format("+{0}", vipPoint.ToString());
            if (actionItem.valueGroup.txt_vipPoint != null)
                actionItem.valueGroup.txt_vipPoint.text = string.Format("+{0}", vipPoint.ToString());
        }

        private void ClosePage()
        {
        }

        private void OnEnable()
        {
            ShopModel.OnPurchaseComplete += OnPurchaseComplete;
        }

        private void OnDisable()
        {
            ShopModel.OnPurchaseComplete -= OnPurchaseComplete;
        }

        #endregion Unimportant Code

        /// <summary>
        /// 檢查是否還有可買的商品
        /// </summary>
        public bool CheckProductAvailable()
        {
            bool hasProduct = false;
            bool onOff = false;

            string myString = "";

            if (myString == null)
            {
            }

#if  UNITY_ANDROID || UNITY_IOS

            for (int i = 0; i < _actions.Length; ++i)
            {
                string productId = GetFinalProductId(_actions[i].products);
                ShopModel.ShopItem shopItem = ShopModel.Instance.GetShopItemByProductId(productId);

                if (null != shopItem && onOff)
                //if (null != shopItem)
                {
                    ShopItemInfo shopItemInfo = shopItem.shopItemInfo;

                    var oriCent = shopItemInfo.settingClass == null ? shopItemInfo.baseCoin : shopItemInfo.settingClass.originalCent;

                    if (!IAPManager.Instance.isIAPReady)
                    {
                        _actions[i].button.interactable = false;
                        continue;
                    }

                    FillValueToView(_actions[i],
                        shopItemInfo.usd,
                        oriCent,
                        shopItemInfo.baseCoin,
                        shopItemInfo.vipExp);

                    hasProduct = true;
                    _actions[i].button.interactable = true;
                }
                else
                {
                    _actions[i].button.interactable = false;
                }
            }
#endif

            return hasProduct;
            if (!hasProduct)
            {
                Debug.LogFormat("<color=green>Sold out!!!</color>");
                ClosePage();
            }
        }

        private void Awake()
        {
            // base.Awake();

            for (int i = 0; i < _actions.Length; ++i)
            {
                ProductAction action = _actions[i];
                _actions[i].button.onClick.AddListener(() => OnBuyBtnClick(action));
            }

            if (discountSign != null)
#if UNITY_ANDROID
                discountSign.SetActive(true);
#elif UNITY_IOS
                discountSign.SetActive(false);
#endif

                UpdateWholeView(!IsAnyProductAvailable(false)); // 傳入的參數為 "IsAnyProductAvailable(false)" , 避免 Exception
        }

        private void OnPurchaseComplete(string productName, bool result)
        {
            if (result)
            {
                LoadingIndicator.Instance.Show(useBackground: false, indicator: false);

                var group = new SmartWebCommandGroup();

                ShopModel.Instance.RequestShopItem(group, (error) =>
                {
                    if (!error)
                    {
                        UpdateWholeView(true);
                    }
                    else
                    {
                        ClosePage();
                    }

                    LoadingIndicator.Instance.Close();
                });

                group.Send();
            }
        }

        // This method I decide not to use, because it will cause confusion.

        public bool IsAnyProductAvailable(bool isUpdateData)
        {
            // #if UNITY_ANDROID || UNITY_IOS

            if (isUpdateData)
            {
                UpdateData(false);
            }

            // #endif

            return _hasAnyProduct;
        }

        public void UpdateWholeView(bool isEnableClosePage)
        {
            // #if UNITY_ANDROID || UNITY_IOS
            UpdateData(true);

            if (!_hasAnyProduct && isEnableClosePage)
            {
                ClosePage();
            }

            // #endif
        }

        /////////////////////////////////////////////////////////////////////////////////

        public void UpdateData(bool isUpdateProductsGui)
        {
            // #if UNITY_ANDROID || UNITY_IOS

            var isIapReady = IAPManager.Instance.isIAPReady;

            for (int i = 0; i < _actions.Length; ++i)
            {
                var productId = GetFinalProductId(_actions[i].products);
                var shopItem = ShopModel.Instance.GetShopItemByProductId(productId);

                var canBuy = shopItem != null && isIapReady;

                if (canBuy && isUpdateProductsGui)
                {
                    var shopItemInfo = shopItem.shopItemInfo;
                    var oriCent = shopItemInfo.settingClass == null
                        ? shopItemInfo.baseCoin
                        : shopItemInfo.settingClass.originalCent;

                    FillValueToView(_actions[i],
                        shopItemInfo.usd,
                        oriCent,
                        shopItemInfo.baseCoin,
                        shopItemInfo.vipExp);
                }

                _actions[i].button.interactable = canBuy;
                _hasAnyProduct |= canBuy;
            }

            // #endif
        }
    }
}