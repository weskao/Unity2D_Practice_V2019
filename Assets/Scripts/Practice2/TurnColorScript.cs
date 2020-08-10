using UnityEngine;

namespace Practice2
{
    public class TurnColorScript : MonoBehaviour
    {
        private void OnEnable()
        {
            EventManager.OnClicked += TurnColor;
        }

        private void OnDisable()
        {
            EventManager.OnClicked -= TurnColor;
        }

        private void TurnColor()
        {
            var color = new Color(Random.value, Random.value, Random.value);
            GetComponent<Renderer>().material.color = color;
        }
    }
}