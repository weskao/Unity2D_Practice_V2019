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
            foreach (var toggle in _toggles)
            {
                toggle.group = _toggleGroup;
                toggle.onValueChanged.AddListener(OnToggleValueChange);
            }
        }

        private void OnToggleValueChange(bool isOn)
        {
            if (isOn)
            {
                var activeToggle = _toggleGroup.ActiveToggles().First();
                Debug.Log($"OnToggleValueChange() - current activeToggle.name = {activeToggle.name}");
            }
        }
    }
}