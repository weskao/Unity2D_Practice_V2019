using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Practice7_EventSystem
{
    // Code from: https://learn.unity.com/tutorial/working-with-the-event-system#5fb9715fedbc2a36e7114985
    public class UI_ColorPicker : MonoBehaviour
    {
        [SerializeField]
        private GameObject _target;

        [SerializeField]
        private TextMeshProUGUI _tmpColorChangeCount;

        private Material _targetMaterial;
        private GraphicRaycaster _graphicRaycaster;
        private PointerEventData _pointerEventData;
        private EventSystem _eventSystem;

        private int _colorChangeCount;

        private void Start()
        {
            // Obtain the target object's material
            if (_target != null)
            {
                _targetMaterial = _target.GetComponent<Image>().material;
            }

            // Obtain the Canvas's Raycaster and EventSystem Components
            _graphicRaycaster = GetComponent<GraphicRaycaster>();
            _eventSystem = GetComponent<EventSystem>();
        }

        private void Update()
        {
            _pointerEventData = new PointerEventData(_eventSystem);
            // _pointerEventData.position = Input.mousePosition; // Hover

#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
            {
                _pointerEventData.position = Input.mousePosition;

#else
                        if(Input.touchCount > 0)
                        {
                        _pointerEventData.position = Input.GetTouch(0).position;
#endif

                var raycastResultList = new List<RaycastResult>();
                _graphicRaycaster.Raycast(_pointerEventData, raycastResultList);

                foreach (RaycastResult swatch in raycastResultList)
                {
                    var targetColor = swatch.gameObject.GetComponent<Image>().color;
                    if (targetColor != null)
                    {
                        ChangeColor(_targetMaterial, targetColor);
                    }
                }
            }
        }

        private void ChangeColor(Material targetMaterial, Color targetColor)
        {
            Debug.Log($"ChangeColor() - targetColor = RGB({targetColor.r}, {targetColor.g}, {targetColor.b})");
            AddColorChangeCount();
            targetMaterial.color = targetColor;
        }

        private void AddColorChangeCount()
        {
            _colorChangeCount++;
            _tmpColorChangeCount.text = _colorChangeCount.ToString();
        }
    }
}