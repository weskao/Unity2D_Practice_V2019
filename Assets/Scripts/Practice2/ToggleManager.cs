using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Practice2
{
    public class ToggleManager : MonoBehaviour
    {
        [SerializeField]
        private Text _displayText;

        [SerializeField]
        private ToggleGroup _toggleGroup;

        [SerializeField]
        private Toggle CurrentSelection => _toggleGroup.ActiveToggles().FirstOrDefault();

        [SerializeField]
        private Toggle _currentToggle;

        private void Start()
        {
            // way1: Static bind listener
            // if (_currentToggle != null)
            // {
            //     _currentToggle.onValueChanged.AddListener(isOn => CategoryToggleChanged());
            //
            //     Debug.Log($"Current toggle name is: {_currentToggle.name}");
            //     Debug.Log($"Bind \"onValueChanged\" event - {nameof(CategoryToggleChanged)} for {_currentToggle.name}");
            // }

            // way2: Dynamic binding
            // GameObject.Find("Toggle_Red").GetComponent<Toggle>().onValueChanged.AddListener(isOn => CategoryToggleChanged());

            // bind IsOn event
            // var toggleRed = GameObject.Find("Toggle_Red").GetComponent<Toggle>();
            // toggleRed.onValueChanged.AddListener(isOn => Debug.Log(isOn ? "Open" : "Close"));
            //
            // Debug.LogFormat($"<color=green>Current select option is:{CurrentSelection}</color>");
        }

        public void CategoryToggleChanged()
        {
            Debug.Log($"Current Selection is : {_toggleGroup.ActiveToggles().FirstOrDefault()}");
        }

        public void IsOn()
        {
            if (_currentToggle != null)
            {
                Debug.LogFormat(_currentToggle.isOn ? "<color=blue>On</color>" : "Off");
            }
        }
    }
}