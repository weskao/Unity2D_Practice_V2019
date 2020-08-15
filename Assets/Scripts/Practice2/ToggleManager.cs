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
            if (_currentToggle != null)
            {
                _currentToggle.onValueChanged.AddListener(isOn => PrintInfo());

                Debug.Log($"Current toggle name is: {_currentToggle.name}");
                Debug.Log($"Bind \"onValueChanged\" event - {nameof(PrintInfo)} for {_currentToggle.name}");
            }

            // way2: Dynamic binding
            // GameObject.Find("Toggle_Red").GetComponent<Toggle>().onValueChanged.AddListener(isOn => PrintInfo());

            Debug.LogFormat($"<color=green>Current select option is:{CurrentSelection}</color>");
        }

        public void PrintInfo()
        {
            Debug.Log("Toggle value changed!");
        }
    }
}