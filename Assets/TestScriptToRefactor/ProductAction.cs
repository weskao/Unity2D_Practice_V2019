using System;
using UnityEngine.UI;

namespace TestScriptToRefactor
{
    [Serializable]
    public class ProductAction
    {
        public ProductInfo[] products;
        public Button button;
        public ValueGroup valueGroup;
    }
}