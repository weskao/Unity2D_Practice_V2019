using Practice3_ObjectPool;
using UnityEngine;

namespace Practice2
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private GameObject _bulletPrefab;

        [SerializeField]
        private ObjectPoolComponent<Bullet> _bulletPoolComponent;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var bullet = _bulletPoolComponent.RequestGeneratedObject();

                // Communicate with the object pool system
                // Request bullet
            }
        }
    }
}