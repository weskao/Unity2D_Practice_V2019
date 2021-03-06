﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Practice5
{
    public class LevelUpSequencePanel : MonoBehaviour
    {
        private Queue<IEnumerator> _levelUpPlayingQueue = new Queue<IEnumerator>();
        private Coroutine _playingCoroutine;
        private static int _testNum = 0; // only for test

        private void Awake()
        {
        }

        public void StartCoroutineInQueue()
        {
            Debug.LogFormat("<color=green>StartCoroutineInQueue</color>");

            if (HasDataWaitingForPlay())
            {
                gameObject.SetActive(true);
                PlaySequentially();
            }
        }

        private void PlaySequentially()
        {
            while (HasDataWaitingForPlay())
            {
                Debug.Log("_levelUpPlayingQueue.Count = " + _levelUpPlayingQueue.Count);
                DoNextReq();
                _testNum++;

                Debug.LogFormat("<color=green>===============</color>");

                if (_testNum > 3)
                {
                    Debug.LogError("<color=green>Infinite!!! Error</color>");
                    break;
                }
            }
        }

        private bool HasDataWaitingForPlay()
        {
            return _levelUpPlayingQueue.Count > 0;
        }

        public void Enqueue(LevelUpRewardItem levelUpRewardItem, Action callback)
        {
            Debug.Log($"Enqueue()");

            _levelUpPlayingQueue.Enqueue(EnqueueShow(levelUpRewardItem, callback));
        }

        public void DoNextReq()
        {
            Debug.Log($"DoNextReq");

            _playingCoroutine = StartCoroutine(_levelUpPlayingQueue.Dequeue());
        }

        private IEnumerator EnqueueShow(LevelUpRewardItem levelUpRewardItem, Action callback)
        {
            Debug.Log($"EnqueueShow()");

            // yield return Show(levelUpRewardItem);
            // callback.Invoke();

            yield return ShowWithCallback(levelUpRewardItem, callback);
        }

        private IEnumerator Show(LevelUpRewardItem levelUpRewardItem)
        {
            Debug.LogFormat("<color=green>Show()</color>");

            yield break;
        }

        private IEnumerator ShowWithCallback(LevelUpRewardItem levelUpRewardItem, Action callback)
        {
            Debug.LogFormat("<color=green>ShowWithCallback()</color>");

            callback.Invoke();

            yield return 1;
        }
    }
}