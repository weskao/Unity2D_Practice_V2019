using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

namespace TestScript
{
    public class ActivityBuyProductActionBase : MonoBehaviour
    {
        #region Unimportant Code

        [SerializeField]
        protected ProductAction[] _actions = null;

        //目前ios不得出現discountSign, 故作此暫時方案
        [SerializeField]
        private GameObject discountSign = null;

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
        private bool _hasAnyProduct = false;

        protected virtual void OnBuyBtnClick(ProductAction action)
        {
            //string productId = GetFinalProductId(action.products);

            string productId;

            if (_isOneClickUpgradeLevelEnabled)
            {
                _fakeVIPLevel = _fakeVIPLevel % 12;

                var ids = new[] { "scg.promote.vip1.599.500000", "scg.promote.vip2.599.540000", "scg.promote.vip3.599.600000", "scg.promote.vip4.599.680000", "scg.promote.vip5.599.760000", "scg.promote.vip6.599.820000", "scg.promote.vip7.599.920000", "scg.promote.vip8.599.1200000", "scg.promote.vip9.599.1360000", "scg.promote.vip10.599.1600000", "scg.promote.vip11.599.2000000", "scg.promote.vip12.599.2400000" };
                productId = ids[_fakeVIPLevel] + ".android";

                _fakeVIPLevel++;
            }
            else
            {
                productId = GetFinalProductId(action.products);
            }

            ShopModel.Instance.BuyProductID(productId, action.button.gameObject);

            Debug.Log(string.Format("OnBuyBtnClick(), productId = {0}", productId));
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
            //

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

                RefreshWholeView(true);
            //CheckProductAvailable();
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
                        RefreshWholeView(true);
                        //CheckProductAvailable();
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
        public bool IsAnyProductAvailable(bool isEnableClosePage, bool isUpdateProductsGui)
        {
            // bool onOff = false;

            // #if UNITY_ANDROID || UNITY_IOS

            RefreshData(isUpdateProductsGui);
            // #endif

            if (!_hasAnyProduct && isEnableClosePage)
            {
                Debug.LogFormat("<color=green>Sold out!!!</color>");
                ClosePage();
            }

            return _hasAnyProduct;
        }

        /// <summary>
        /// /////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        /// <returns></returns>

        public bool IsAnyProductAvailable()
        {
            // #if UNITY_ANDROID || UNITY_IOS

            RefreshData(false);
            // #endif

            return _hasAnyProduct;
        }

        public void RefreshWholeView(bool isEnableClosePage)
        {
            // #if UNITY_ANDROID || UNITY_IOS
            RefreshData(true);

            if (!_hasAnyProduct && isEnableClosePage)
            {
                ClosePage();
            }

            // #endif
        }

        public void RefreshData(bool isUpdateProductsGui)
        {
            // #if UNITY_ANDROID || UNITY_IOS
            for (int i = 0; i < _actions.Length; ++i)
            {
                var shopItem = ShopModel.Instance.GetShopItemByProductId(GetFinalProductId(_actions[i].products));

                var isIAPReady = IAPManager.Instance.isIAPReady; // Question: Did this need to be checked every time?

                var canBuy = shopItem != null && IAPManager.Instance.isIAPReady;

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
            }

            _hasAnyProduct = _actions.Any(x => x.button.interactable.Equals(true));

            // #endif
        }
    }
}