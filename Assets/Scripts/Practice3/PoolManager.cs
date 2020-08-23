using System.Collections.Generic;
using Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Practice3
{
    // Unity Challenge: PoolManager - https://reurl.cc/Ez7k1k
    public class PoolManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject _bulletContainer;

        [SerializeField]
        private GameObject _bulletPrefab;

        [SerializeField]
        private List<GameObject> _bulletPool;

        private int _amountOfBullets = 10;

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
            _bulletPool = GenerateBullets(_amountOfBullets);
        }

        public GameObject RequestBullet()
        {
            foreach (var bullet in _bulletPool)
            {
                if (bullet.activeInHierarchy == false)
                {
                    bullet.Show();
                    return bullet;
                }
            }

            return CreateBullet(_bulletPool.Count);
        }

        private List<GameObject> GenerateBullets(int amountOfBullets)
        {
            for (var i = 0; i < amountOfBullets; i++)
            {
                var bullet = CreateBullet(i);

                bullet.Hide();
            }

            return _bulletPool;
        }

        private GameObject CreateBullet(int offsetY)
        {
            var bullet = Instantiate(_bulletPrefab);
            var oldBulletPosition = bullet.transform.position;
            var newPositionY = oldBulletPosition.y - bullet.GetComponent<Image>().sprite.rect.height * offsetY;

            bullet.transform.position = new Vector2(oldBulletPosition.x, newPositionY);
            bullet.transform.parent = _bulletContainer.transform;

            _bulletPool.Add(bullet);

            return bullet;
        }
    }
}