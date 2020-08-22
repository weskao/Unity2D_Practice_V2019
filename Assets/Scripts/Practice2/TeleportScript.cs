using System;
using System.Reflection;
using Practice2.Manager;
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
            EventManager.OnClicked += Teleport;

            foreach (var buttonObject in _buttonObjects)
            {
                buttonObject.button.onClick.AddListener(() => AddNumber(buttonObject.name));
            }
        }

        private void OnDestroy()
        {
            EventManager.OnClicked -= Teleport;

            foreach (var buttonObject in _buttonObjects)
            {
                buttonObject.button.onClick.RemoveListener(() => AddNumber(buttonObject.name));
            }
        }

        private void AddNumber(string buttonName)
        {
            _number++;
            Debug.Log($"Invoke {buttonName}.{MethodBase.GetCurrentMethod().Name} method, number = {_number}");
        }

        private void Teleport()
        {
            var position = transform.position;

            if (Camera.main != null)
            {
                var screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight, 0));
                position.y = UnityEngine.Random.Range(0f, screenPosition.y / 2);
            }

            transform.position = position;
        }
    }
}