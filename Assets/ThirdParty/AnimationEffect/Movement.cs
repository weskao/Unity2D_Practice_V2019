using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private Transform _targetTransform;

    [SerializeField]
    private Button _triggerButton;

    private bool _direction = false;

    private void Awake()
    {
        _triggerButton.onClick.AddListener(OnTriggerButtonClick);
        SetTargetTransformObject();
    }

    private void SetTargetTransformObject()
    {
        if (_targetTransform == null)
        {
            _targetTransform = transform;
        }
    }

    private void OnDestroy()
    {
        _triggerButton.onClick.RemoveListener(OnTriggerButtonClick);
    }

    private void OnTriggerButtonClick()
    {
        Debug.LogFormat("<color=yellow>Wes - Movement - OnTriggerButtonClick()</color>");

        Debug.Log($"Wes - _direction = {_direction}");
        if (_direction)
        {
            _targetTransform.DORewind();
        }
        else
        {
            _targetTransform.DOMoveX(0.5f, 1).From();
        }

        _direction = !_direction;
    }
}