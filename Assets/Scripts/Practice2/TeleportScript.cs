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

            foreach (var buttonObject in _buttonObjects)
            {
                buttonObject.button.onClick.AddListener(() => AddNumber(buttonObject.name));
            }
        }

        private void OnDestroy()
        {
            EventManager.OnClicked -= Teleoprt;

            foreach (var buttonObject in _buttonObjects)
            {
                buttonObject.button.onClick.RemoveListener(() => AddNumber(buttonObject.name));
            }
        }

        private void AddNumber(string buttonName)
        {
            _number++;
            Debug.Log(string.Format("Invoke {0}.{1} method, number = {2}",
                buttonName, MethodBase.GetCurrentMethod().Name, _number));
        }

        private void Teleoprt()
        {
            var position = transform.position;
            position.y = UnityEngine.Random.Range(1.0f, 3f);
            transform.position = position;
        }
    }
}