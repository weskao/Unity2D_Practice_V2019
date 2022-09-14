using System;
using System.Linq;
using DG.Tweening;
using Practice8;
using UnityEngine;
using UnityEngine.UI;

namespace Practice7_EventSystem
{
    public class ScatterWinNumberSlider : MonoBehaviour
    {
        [SerializeField]
        private ScrollRect scrollRect = null;

        [SerializeField]
        private float moveDuration = 1.5f;

        private const int FIRST_ITEM_INDEX = 1;

        private int itemCount = 0;

        private float targetSlideValue = 0;

        public Action onMoveStart;
        public Action onMoveComplete;

        private bool isMoving;

        private void Awake()
        {
            scrollRect.horizontal = true;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                Debug.Log($"Wes - [Slider] - scrollRect.horizontalNormalizedPosition = {scrollRect.horizontalNormalizedPosition}");
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                itemCount = GetComponentsInChildren<ScatterWinNumber>().Count(x => x.IsVisible());

                Slide(itemCount);
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                itemCount = GetComponentsInChildren<ScatterWinNumber>().Count(x => x.IsVisible());

                Debug.LogFormat($"<color=yellow>Wes - [ScatterWinNumberSlider] - Move to front!</color>");
                Slide(FIRST_ITEM_INDEX);
            }
        }

        private void Slide(int itemIndex)
        {
            if (isMoving)
            {
                return;
            }

            isMoving = true;
            targetSlideValue = GetSlideTargetValue(itemIndex);
            onMoveStart?.Invoke();
            Debug.Log($"Wes - [Slider] - _pageCount = {itemCount}, _targetValue = {targetSlideValue}");

            StartToSlide(targetSlideValue, moveDuration, () =>
            {
                isMoving = false;
                Debug.LogFormat($"<color=yellow>Wes - [Slider] - Move complete</color>");
                onMoveComplete?.Invoke();
            });
        }

        private void StartToSlide(float targetValue, float duration, Action onComplete = null)
        {
            scrollRect.DOHorizontalNormalizedPos(targetValue, duration)
                .OnComplete(() =>
                {
                    onComplete?.Invoke();
                });
        }

        private float GetSlideTargetValue(int itemIndex)
        {
            float eachRange = (itemCount <= 1) ? 1 : 1f / (itemCount - 1);

            if (itemIndex > itemCount)
            {
                itemIndex = itemCount;
            }

            return (itemIndex - 1) * eachRange;
        }

        private static int GetSecondLastItemIndex(int itemIndex)
        {
            var index = itemIndex - 2;
            return index > 0 ? index : 0;
        }
    }
}