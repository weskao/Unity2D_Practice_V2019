using System;
using UnityEngine;

namespace TestScriptToRefactor.TestCaseOne
{
    public class ShopModel
    {
        public static Action<string, bool> OnPurchaseComplete;

        public static ShopModel Instance => new ShopModel();

        public void RequestShopItem(SmartWebCommandGroup @group, Action<bool> action)
        {
        }

        public void BuyProductID(string productId, GameObject buttonGameObject)
        {
        }

        public class ShopItem
        {
            public ShopItemInfo shopItemInfo;
        }

        public ShopItem GetShopItemByProductId(string productId)
        {
            return new ShopItem();
        }
    }
}