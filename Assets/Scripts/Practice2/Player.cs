﻿using Practice3_ObjectPool;
using UnityEngine;

namespace Practice2
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private GameObject _bulletPrefab;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var bullet = PoolManager.Instance.RequestGeneratedObject();

                // Communicate with the object pool system
                // Request bullet
            }
        }
    }
}