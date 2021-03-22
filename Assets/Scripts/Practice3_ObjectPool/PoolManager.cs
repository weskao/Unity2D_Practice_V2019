using Extensions;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Practice3_ObjectPool
{
    // Unity Challenge: PoolManager - https://reurl.cc/Ez7k1k
    public class PoolManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject _objectContainer;

        [SerializeField]
        private GameObject _generatedObject;

        [SerializeField]
        private int _basicAmountOfGeneratedObject = 2;

        [SerializeField]
        private int _additionalAmountOfGeneratedObject = 8;

        private List<GameObject> _objectPool;

        private static PoolManager _instance;

        public static PoolManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError("The PoolManager is NULL");
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
            _objectPool = GenerateObject(_basicAmountOfGeneratedObject);
            _objectPool.AddRange(GenerateObject(_additionalAmountOfGeneratedObject));
        }

        public GameObject RequestGeneratedObject()
        {
            foreach (var generatedObject in _objectPool)
            {
                if (generatedObject.activeInHierarchy == false)
                {
                    generatedObject.Show();
                    return generatedObject;
                }
            }

            return GetNewObject(_objectPool.Count);
        }

        private List<GameObject> GenerateObject(int amountOfGeneratedObject)
        {
            var generatedObjectList = new List<GameObject>();

            for (var i = 0; i < amountOfGeneratedObject; i++)
            {
                var generatedObject = GetNewObject(i);

                generatedObject.Hide();
                generatedObjectList.Add(generatedObject);
            }

            return generatedObjectList;
        }

        private GameObject GetNewObject(int offsetY)
        {
            var generatedObject = Instantiate(_generatedObject);
            var oldObjectPosition = generatedObject.transform.position;
            var newPositionY = oldObjectPosition.y - generatedObject.GetComponent<Image>().sprite.rect.height * offsetY;

            generatedObject.transform.position = new Vector2(oldObjectPosition.x, newPositionY);
            generatedObject.transform.parent = _objectContainer.transform;

            return generatedObject;
        }
    }
}