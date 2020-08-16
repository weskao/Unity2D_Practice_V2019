using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ThirdParty.ToggleGroupExtensions
{
    [RequireComponent(typeof(ToggleGroup))]
    public class EventToggleGroup : MonoBehaviour
    {
        [Serializable]
        public class ToggleEvent : UnityEvent<Toggle>
        {
        }

        [SerializeField]
        public ToggleEvent onActiveToggleChanged;

        [SerializeField]
        private Toggle[] _toggles;

        private ToggleGroup _toggleGroup;

        private void Awake()
        {
            _toggleGroup = GetComponent<ToggleGroup>();
        }

        private void OnEnable()
        {
            foreach (var toggle in _toggles)
            {
                if (toggle.group != null && toggle.group != _toggleGroup)
                {
                    Debug.LogError("EventToggleGroup is trying to register a Toggle that is a member of another group.");
                }

                toggle.group = _toggleGroup;
                toggle.onValueChanged.AddListener(HandleToggleValueChanged);
            }
        }

        private void HandleToggleValueChanged(bool isOn)
        {
            if (isOn)
            {
                onActiveToggleChanged?.Invoke(_toggleGroup.ActiveToggles().FirstOrDefault());
            }
        }

        private void OnDisable()
        {
            foreach (var toggle in _toggleGroup.ActiveToggles())
            {
                toggle.onValueChanged.RemoveListener(HandleToggleValueChanged);
                toggle.group = null;
            }
        }
    }
}