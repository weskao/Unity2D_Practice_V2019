using System;
using System.Collections;
using UnityEngine;

namespace Practice6
{
    public class TestRetry : MonoBehaviour
    {
        private int _retryTimes = 0;

        private IEnumerator _retryConfirmAdReward = null;

        private const int MAX_RETRY_TIME = 3;

        private void Start()
        {
            StartRetry();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                StartRetry();
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                StopRetry();
            }
        }

        private IEnumerator Retry(Action doNextAction)
        {
            while (_retryTimes <= MAX_RETRY_TIME || _retryConfirmAdReward != null)
            {
                _retryTimes++;
                Debug.Log($"Wes - LevelUpPanel - Retry() - _retryTimes = {_retryTimes}");
                doNextAction.Invoke();

                yield return new WaitForSeconds(MAX_RETRY_TIME);

                if (_retryTimes > MAX_RETRY_TIME)
                {
                    yield break;
                }
            }
        }

        private void RetrySendGetRewardWithAdBonusRequest(LevelUpInfo levelUpInfo)
        {
            var isServerReceiveWatchAdsDoneMsg = false;

            Debug.LogFormat("<color=yellow>Wes - LevelUpPanel - Set isServerReceiveWatchAdsDoneMsg = false</color>");

            if (isServerReceiveWatchAdsDoneMsg)
            {
                Debug.LogFormat("<color=yellow>TestRetry - LevelService.Instance.SendTakeBasicAndAdsBonusRewardRequest()</color>");

                StopRetry();
            }
            else
            {
                Debug.Log($"<color=red>[WebService] Retry: {_retryTimes} </color>");

                if (_retryTimes >= MAX_RETRY_TIME)
                {
                    Debug.LogFormat("<color=yellow>TestRetry - LevelService.Instance.SendTakeBasicAndAdsBonusRewardRequest()</color>");

                    StopRetry();
                }
            }
        }

        private void StartRetry()
        {
            if (_retryConfirmAdReward != null)
            {
                Debug.LogWarning($"Already Started! Can't start it in the same time.");
                return;
            }

            Debug.LogFormat("<color=green>TestRetry - StartRetry()</color>");

            var levelUpInfo = new LevelUpInfo();

            _retryConfirmAdReward = Retry(() =>
            {
                RetrySendGetRewardWithAdBonusRequest(levelUpInfo);
            });

            StartCoroutine(_retryConfirmAdReward);
        }

        private void StopRetry()
        {
            Debug.LogFormat("<color=green>TestRetry - StopRetry()</color>");
            StopCoroutine(_retryConfirmAdReward);
            Init();
        }

        private void Init()
        {
            _retryConfirmAdReward = null;
            _retryTimes = 0;
        }
    }
}