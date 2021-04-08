using System;
using System.Collections;
using UnityEngine;

namespace Practice2
{
    public class TestRetryComponent : MonoBehaviour
    {
        // private bool _isEndRetry;
        private const int MAX_RETRY_TIMES = 3;

        private IEnumerator _testRetryRoutine;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                BreakRetry();
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                Init();

                _testRetryRoutine = TestRetry();
                StartCoroutine(_testRetryRoutine);

                Debug.LogFormat("<color=orange>Start TestRetry()</color>");
                Debug.LogFormat("<color=orange>TestRetryComponent - Start TestRetry()</color>");
            }
        }

        private void BreakRetry()
        {
            // _isEndRetry = true;
            if (_testRetryRoutine != null)
            {
                Debug.LogFormat("<color=orange>TestRetryComponent - BreakRetry</color>");
                StopCoroutine(_testRetryRoutine);
            }
        }

        private void Init()
        {
            // _isEndRetry = false;
        }

        private IEnumerator TestRetry()
        {
            // for (var i = 1; i <= MAX_RETRY_TIMES && _isEndRetry == false; i++)
            for (var i = 1; i <= MAX_RETRY_TIMES; i++)
            {
                Debug.Log($"Retry times: i = {i}");
                yield return new WaitForSeconds(3);
            }

            Debug.LogFormat("<color=orange>TestRetryComponent - TestRetry Done!</color>");
        }
    }
}