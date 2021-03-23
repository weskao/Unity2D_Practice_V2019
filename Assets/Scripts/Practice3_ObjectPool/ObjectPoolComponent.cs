using Extensions;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Practice3_ObjectPool
{
    public class ObjectPoolComponent<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField]
        private T _objectContainer;

        [SerializeField]
        private T _generatedObject;

        [SerializeField]
        private int _basicAmountOfGeneratedObject = 2;

        [SerializeField]
        private int _additionalAmountOfGeneratedObject = 3;

        private int _requestIndex = -1;

        private List<T> _objectPool;

        private static ObjectPoolComponent<T> _instance;

        public static ObjectPoolComponent<T> Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError("The ObjectPoolComponent is NULL");
                }

                return _instance;
            }
        }

        private void Awake()
        {
            _instance = this;
        }

        private void Start()
        {
            _objectPool = GenerateObjectList(_basicAmountOfGeneratedObject);
            // _objectPool.AddRange(GenerateObjectList(_additionalAmountOfGeneratedObject));
            Debug.Log($"Wes - Start() - _objectPool.Count = {_objectPool.Count}");
        }

        public T RequestGeneratedObject()
        {
            Debug.LogFormat("<color=yellow>Wes - ObjectPoolComponent - RequestGeneratedObject()</color>");

            _requestIndex++;

            if (_requestIndex >= _objectPool.Count)
            {
                _objectPool.Add(GetNewObject());
            }

            Debug.Log($"Wes - _requestCount = {_requestIndex}, _objectPool.Count = {_objectPool.Count}");

            Debug.Log($"Wes - RequestGeneratedObject() - _objectPool.Count = {_objectPool.Count}");

            return _objectPool[_requestIndex];
        }

        private List<T> GenerateObjectList(int objectAmount)
        {
            var generatedObjectList = new List<T>();

            for (var i = 0; i < objectAmount; i++)
            {
                var generatedObject = GetNewObject();

                generatedObject.Hide();
                generatedObjectList.Add(generatedObject);
            }

            return generatedObjectList;
        }

        private T GetNewObject()
        {
            var generatedObject = Instantiate(_generatedObject, _objectContainer.transform, true);
            // var oldObjectPosition = generatedObject.transform.position;
            // var newPositionY = oldObjectPosition.y - generatedObject.GetComponent<Image>().sprite.rect.height * offsetY;
            //
            // generatedObject.transform.position = new Vector2(oldObjectPosition.x, newPositionY);

            return generatedObject;
        }
    }
}