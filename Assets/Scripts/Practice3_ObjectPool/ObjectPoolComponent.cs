using System.Collections.Generic;
using UnityEngine;

namespace Practice3_ObjectPool
{
    public class ObjectPoolComponent<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField]
        private T _generatedObject;

        [SerializeField]
        private int _basicAmountOfGeneratedObject = 0;

        // [SerializeField]
        // private int _additionalAmountOfGeneratedObject = 3;

        public int RequestIndex { get; private set; } = -1;

        private List<T> _objectPool;

        public T RequestGeneratedObject()
        {
            Debug.LogFormat("<color=yellow>Wes - ObjectPoolComponent - RequestGeneratedObject()</color>");

            RequestIndex++;

            if (RequestIndex + 1 > _objectPool.Count)
            {
                _objectPool.Add(GetNewObject());
            }

            // foreach (var generatedObject in _objectPool)
            // {
            //     var existedObject = ((GameObject)Convert.ChangeType(generatedObject, typeof(GameObject)));
            //     if (existedObject.activeInHierarchy == false)
            //     {
            //         return existedObject;
            //         // generatedObject.Show();
            //         // return generatedObject;
            //     }
            // }

            Debug.Log($"Wes - ObjectPoolComponent - RequestGeneratedObject() - _objectPool.Count = {_objectPool.Count}");

            return _objectPool[RequestIndex];
        }

        public void OnEnable()
        {
            Init();
        }

        public void OnDisable()
        {
            HideAllObjectsInPool();
            // _objectPool = null;
        }

        // private void OnDestroy()
        // {
        //     _objectPool = null;
        // }

        public void Init()
        {
            Debug.LogFormat("<color=yellow>Wes - ObjectPoolComponent - Init()</color>");
            RequestIndex = -1;

            // _objectPool = GetComponentsInChildren<T>().ToList()

            if (_objectPool != null)
            {
                HideAllObjectsInPool();
            }
            else
            {
                _objectPool = GenerateObjectList(_basicAmountOfGeneratedObject);
            }
        }

        private void HideAllObjectsInPool()
        {
            foreach (var objectInPool in _objectPool)
            {
                objectInPool.gameObject.SetActive(false);
            }
        }

        private List<T> GenerateObjectList(int objectAmount)
        {
            var generatedObjectList = new List<T>();

            for (var i = 0; i < objectAmount; i++)
            {
                var generatedObject = GetNewObject();

                generatedObject.gameObject.SetActive(false);
                generatedObjectList.Add(generatedObject);
            }

            return generatedObjectList;
        }

        private T GetNewObject()
        {
            return Instantiate(_generatedObject, gameObject.transform, true);
        }
    }
}