using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Practice3
{
    public class LevelUpSequencePanel : MonoBehaviour
    {
        private Action _onComplete;
        private Queue<IEnumerator> _levelUpPlayingQueue = new Queue<IEnumerator>();
        private Coroutine _playingCoroutine;

        private void Awake()
        {
        }

        public void StartCoroutineInQueue()
        {
            Debug.LogFormat("<color=green>StartCoroutineInQueue</color>");
        }

        public void Enqueue(LevelUpRewardItem levelUpRewardItem, Action callback)
        {
            Debug.Log($"Enqueue()");

            _levelUpPlayingQueue.Enqueue(EnqueueShow(levelUpRewardItem, _onComplete));
            _onComplete = callback;
        }

        public void DoNextReq()
        {
            Debug.Log($"DoNextReq");

            _playingCoroutine = StartCoroutine(_levelUpPlayingQueue.Dequeue());
        }

        private IEnumerator EnqueueShow(LevelUpRewardItem levelUpRewardItem, Action callback)
        {
            Debug.Log($"EnqueueShow()");

            yield return Show(levelUpRewardItem);

            callback.Invoke();
        }

        private IEnumerator Show(LevelUpRewardItem levelUpRewardItem)
        {
            Debug.LogFormat("<color=yellow>Show()</color>");

            yield break;
        }
    }
}