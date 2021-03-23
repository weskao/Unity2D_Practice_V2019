using Extensions;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Practice3_ObjectPool
{
    public class ObjectPoolComponent : MonoBehaviour
    {
        [SerializeField]
        private GameObject _objectContainer;

        [SerializeField]
        private GameObject _generatedObject;

        [SerializeField]
        private int _basicAmountOfGeneratedObject = 2;

        [SerializeField]
        private int _additionalAmountOfGeneratedObject = 3;

        private List<GameObject> _objectPool;

        private static ObjectPoolComponent _instance;

        public static ObjectPoolComponent Instance
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
            _objectPool.AddRange(GenerateObjectList(_additionalAmountOfGeneratedObject));
        }

        public GameObject RequestGeneratedObject()
        {
            Debug.LogFormat("<color=yellow>Wes - ObjectPoolComponent - RequestGeneratedObject()</color>");
            foreach (var generatedObject in _objectPool)
            {
                if (generatedObject.activeInHierarchy == false)
                {
                    generatedObject.Show();
                    return generatedObject;
                }
            }

            return GetNewObject();
        }

        private List<GameObject> GenerateObjectList(int objectAmount)
        {
            var generatedObjectList = new List<GameObject>();

            for (var i = 0; i < objectAmount; i++)
            {
                var generatedObject = GetNewObject();

                generatedObject.Hide();
                generatedObjectList.Add(generatedObject);
            }

            return generatedObjectList;
        }

        private GameObject GetNewObject()
        {
            var generatedObject = Instantiate(_generatedObject);
            // var oldObjectPosition = generatedObject.transform.position;
            // var newPositionY = oldObjectPosition.y - generatedObject.GetComponent<Image>().sprite.rect.height * offsetY;
            //
            // generatedObject.transform.position = new Vector2(oldObjectPosition.x, newPositionY);
            // generatedObject.transform.parent = _objectContainer.transform;

            return generatedObject;
        }
    }
}