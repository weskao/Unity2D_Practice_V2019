using TMPro;
using UnityEngine;

namespace Practice8
{
    public class ScatterWinNumber : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI tmpWinNumber = null;

        public bool IsVisible()
        {
            return !tmpWinNumber.text.Equals(string.Empty);
        }
    }
}