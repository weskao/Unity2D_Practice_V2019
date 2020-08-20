using UnityEngine;
using UnityEngine.UI;

namespace Practice4
{
    public class BoxManager : MonoBehaviour
    {
        [SerializeField]
        private Sprite[] _sprites;

        [SerializeField]
        private Image _controlImage;

        private int _currentSpriteIndex = 0;

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