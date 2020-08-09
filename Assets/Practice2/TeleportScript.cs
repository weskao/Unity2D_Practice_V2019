using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace Practice2
{
    [Serializable]
    public class ButtonObject
    {
        public Button button;
        public string name;
    }

    public class TeleportScript : MonoBehaviour
    {
        [SerializeField]
        private ButtonObject[] _buttonObjects;

        private static int _number;

        private void Awake()
        {
            EventManager.OnClicked += Teleoprt;

            for (int i = 0; i < _buttonObjects.Length; i++)
            {
                _buttonObjects[i].name = $"Button{i}";

                var btnName = _buttonObjects[i].name;
                _buttonObjects[i].button.onClick.AddListener(() => AddNumber(btnName));
            }
        }

        private void OnDestroy()
        {
            EventManager.OnClicked -= Teleoprt;

            foreach (var buttonObject in _buttonObjects)
            {
                var btnName = buttonObject.name;
                buttonObject.button.onClick.RemoveListener(() => AddNumber(btnName));
            }
        }

        private void AddNumber(string buttonName)
        {
            _number++;
            Debug.Log($"Invoke {buttonName}.{MethodBase.GetCurrentMethod().Name} method, number = {_number}");
        }

        private void Teleoprt()
        {
            var position = transform.position;
            position.y = UnityEngine.Random.Range(1.0f, 3f);
            transform.position = position;
        }
    }
}