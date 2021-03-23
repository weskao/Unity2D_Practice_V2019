using System;
using Extensions;
using Practice3_ObjectPool;
using UnityEngine;

namespace Practice2
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private GameObject _bulletPrefab;

        // [SerializeField]
        // private ObjectPoolComponent<Bullet> _bulletPoolComponent;

        [SerializeField]
        private BulletPoolComponent _bulletPoolComponent;

        private void Update()
        {
            // string s = KeyCode.Space.ToString();
            // switch (Input.inputString)
            // {
            //     case "1":
            //         break;
            // }
            // Event e = Event.current;
            //
            // if (Input.anyKeyDown && e != null && e.isKey)
            // {
            //     Debug.Log($"Wes - Player - down!");
            //     switch (Event.current.keyCode)
            //     {
            //         case KeyCode.Space:
            //             var bullet = _bulletPoolComponent.RequestGeneratedObject();
            //             bullet.Show();
            //
            //             break;
            //     }
            // }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                var bullet = _bulletPoolComponent.RequestGeneratedObject();
                bullet.Show();
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                _bulletPoolComponent.Init();
            }
        }
    }
}