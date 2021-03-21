using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace ThirdParty.ToggleGroupExtensions
{
    public class EnhancedToggleGroup : ToggleGroup
    {
        private ToggleGroup _toggleGroup;

        private Toggle[] _toggles;

        protected override void Awake()
        {
            _toggleGroup = GetComponent<ToggleGroup>();
            _toggles = _toggleGroup.GetComponentsInChildren<Toggle>();

            InitToggles();
        }

        private void InitToggles()
        {
            Debug.LogFormat("<color=yellow>es - EnhancedToggleGroup - InitToggles()</color>");
            foreach (var toggle in _toggles)
            {
                toggle.group = _toggleGroup;
                toggle.onValueChanged.AddListener(OnToggleValueChange);
                Debug.Log($"toggle.name = {toggle.name}");
            }
        }

        private void OnToggleValueChange(bool isOn)
        {
            if (isOn)
            {
                var activeToggle = _toggleGroup.ActiveToggles().First();
                Debug.Log($"OnToggleValueChange() - activeToggle.name = {activeToggle.name}");
            }
        }
    }
}