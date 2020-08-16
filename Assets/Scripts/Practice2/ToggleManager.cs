using System.Linq;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Toggle = UnityEngine.UI.Toggle;

namespace Practice2
{
    public class ToggleManager : MonoBehaviour
    {
        [SerializeField]
        private Text _displayText;

        [SerializeField]
        private ToggleGroup _toggleGroup;

        private bool _isTextVisible;

        // [SerializeField]
        // private Toggle _currentToggle;

        private void Start()
        {
            _isTextVisible = _displayText.isActiveAndEnabled;

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
            var activeToggle = _toggleGroup.ActiveToggles().FirstOrDefault();
            if (activeToggle != null)
            {
                Debug.LogFormat($"<color=white>Current Selection is : {activeToggle.name}</color>");
                SetTextColor(activeToggle.GetComponentInChildren<Text>().color);
            }
        }

        private void SetTextColor(Color color)
        {
            _displayText.color = color;
        }

        public void ShowHideText()
        {
            _displayText.gameObject.SetActive(!_isTextVisible);
            _isTextVisible = !_isTextVisible;
        }

        public void IsOn()
        {
            // if (_currentToggle != null)
            // {
            //     Debug.LogFormat(_currentToggle.isOn ? "<color=blue>On</color>" : "Off");
            // }
        }
    }
}