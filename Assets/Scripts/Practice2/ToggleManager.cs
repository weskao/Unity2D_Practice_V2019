using System;
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

        private void Start()
        {
            Debug.LogFormat($"<color=green>Current select option is:{CurrentSelection}</color>");
        }
    }
}