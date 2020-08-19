﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Practice3
{
    // Unity Challenge: PoolManager - https://reurl.cc/Ez7k1k
    public class PoolManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject _bulletPrefab;

        [SerializeField]
        private List<GameObject> _bulletPool;

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
            _bulletPool = GenerateBullets(10);
        }

        private List<GameObject> GenerateBullets(int amountOfBullets)
        {
            for (var i = 0; i < amountOfBullets; i++)
            {
                var bullet = Instantiate(_bulletPrefab);
                _bulletPool.Add(bullet);
            }

            return _bulletPool;
        }
    }
}