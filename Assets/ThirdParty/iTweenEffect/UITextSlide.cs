using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UITextSlide : MonoBehaviour
{
    private enum State
    {
        Init,
        Tween,
    }

    private State _currentState = State.Init;
    private TextMeshProUGUI _tmpText = null;
    private RectTransform _rectTransform = null;

    [SerializeField]
    private float _delayToStart = 2f;

    [SerializeField]
    private float _speed = 60f;

    private void Awake()
    {
        _tmpText = GetComponent<TextMeshProUGUI>();
        _rectTransform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        _currentState = State.Init;
        var pos = _rectTransform.anchoredPosition;
        pos.x = 0;
        _rectTransform.anchoredPosition = pos;
        Invoke(nameof(CheckState), _delayToStart);
    }

    private void OnDisable()
    {
        iTween.Stop(gameObject);
        CancelInvoke();
    }

    private void StateChangeToInit()
    {
        _currentState = State.Init;
        Debug.LogFormat("<color=yellow>UITextSlide - StateChangeToInit()</color>");

        iTween.MoveTo(gameObject, iTween.Hash("x", 0,
                                              "time", 0,
                                              "islocal", true,
                                              "easetype", iTween.EaseType.linear,
                                              "delay", 1.5,
                                              "oncomplete", nameof(CheckState)));
    }

    private void StateChangeToTween()
    {
        Debug.LogFormat("<color=yellow>UITextSlide - StateChangeToTween()</color>");

        float diff = _rectTransform.rect.width - _tmpText.preferredWidth;

        Debug.Log($"diff = {diff}");

        if (diff >= 0)
            return;

        _currentState = State.Tween;
        iTween.MoveTo(gameObject, iTween.Hash("x", diff,
                                        "delay", 1,
                                        "speed", _speed,
                                        "easetype", iTween.EaseType.linear,
                                        "islocal", true,
                                        "oncomplete", nameof(CheckState)));
    }

    private void CheckState()
    {
        switch (_currentState)
        {
            case State.Init:
                {
                    StateChangeToTween();
                }
                break;

            case State.Tween:
                {
                    StateChangeToInit();
                }
                break;
        }
    }
}