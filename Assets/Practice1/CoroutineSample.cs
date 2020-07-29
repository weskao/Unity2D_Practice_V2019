using System.Collections;
using UnityEngine;

public class CoroutineSample : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("start");

        StartCoroutine(Exec());

        Debug.Log("end");
    }

    private IEnumerator Exec()
    {
        yield return new WaitForSeconds(5f);
        Debug.Log("step1");

        yield return new WaitForEndOfFrame();
        Debug.Log("step2");

        yield return new WaitForFixedUpdate();
        Debug.Log("step3");

        yield return null;
        Debug.Log("step4");
    }
}