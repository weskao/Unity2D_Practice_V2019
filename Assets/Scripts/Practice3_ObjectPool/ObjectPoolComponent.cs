using Extensions;
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

        [SerializeField]
        private int _additionalAmountOfGeneratedObject = 3;

        private int _requestIndex = -1;

        private List<T> _objectPool;

        // private static ObjectPoolComponent<T> _instance;
        //
        // public static ObjectPoolComponent<T> Instance
        // {
        //     get
        //     {
        //         if (_instance == null)
        //         {
        //             Debug.LogError("The ObjectPoolComponent is NULL");
        //         }
        //
        //         return _instance;
        //     }
        // }

        // private void Awake()
        // {
        //     // _instance = this;
        // }

        private void Start()
        {
            // Init();
            // _objectPool.AddRange(GenerateObjectList(_additionalAmountOfGeneratedObject));
            Debug.Log($"Wes - Start() - _objectPool.Count = {_objectPool.Count}");
        }

        public T RequestGeneratedObject()
        {
            Debug.LogFormat("<color=yellow>Wes - ObjectPoolComponent - RequestGeneratedObject()</color>");

            _requestIndex++;

            if (_requestIndex + 1 > _objectPool.Count)
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

            Debug.Log($"Wes - _requestIndex = {_requestIndex}, _objectPool.Count = {_objectPool.Count}");

            Debug.Log($"Wes - RequestGeneratedObject() - _objectPool.Count = {_objectPool.Count}");

            return _objectPool[_requestIndex];
        }

        public void OnEnable()
        {
            Init();
        }

        public void OnDisable()
        {
            _objectPool = null;
            HideAllObjectsInPool();
        }

        public void Init()
        {
            Debug.LogFormat("<color=yellow>Wes - ObjectPoolComponent - Init()</color>");
            _requestIndex = -1;
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

                generatedObject.Hide();
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