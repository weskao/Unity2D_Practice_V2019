using System;
using UnityEngine;

namespace Practice3_ObjectPool
{
    public class Bullet : MonoBehaviour
    {
        // private void OnEnable()
        // {
        //     Invoke(nameof(Hide), 0);
        // }

        private void Hide()
        {
            Debug.Log("Hide bullet!");
        }

        private void Start()
        {
            Debug.Log("Bullet Created!");
        }
    }
}