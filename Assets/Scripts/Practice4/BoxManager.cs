using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Practice4
{
    public class BoxManager : MonoBehaviour
    {
        private const int IndexToShowShockImage = 2;
        private const float TimeToWaitShowShockImage = 0.3f;

        [SerializeField]
        private Sprite[] _sprites;

        [SerializeField]
        private Image _shockImage;

        [SerializeField]
        private Image _controlImage;

        private int _currentSpriteIndex = 0;

        private void Update()
        {
            if (_shockImage != null)
            {
                if (_currentSpriteIndex == IndexToShowShockImage)
                {
                    StartCoroutine(WaitMoment());
                }
                else
                {
                    _shockImage.gameObject.SetActive(false);

                    StopCoroutine(WaitMoment());
                }
            }
        }

        private IEnumerator WaitMoment()
        {
            yield return new WaitForSeconds(TimeToWaitShowShockImage);

            _shockImage.gameObject.SetActive(true);
        }

        public void OnButtonClicked()
        {
            SwitchToNextImage();
        }

        private void SwitchToNextImage()
        {
            _currentSpriteIndex = _currentSpriteIndex % _sprites.Length;
            _controlImage.sprite = _sprites[_currentSpriteIndex];

            _currentSpriteIndex++;
        }
    }
}