using UnityEngine;

namespace Practice2
{
    public class EventManager : MonoBehaviour
    {
        public delegate void ClickAction();

        public static event ClickAction OnClicked;

        private void OnGUI()
        {
            if (GUI.Button(new Rect((float)Screen.width / 2, 5, 100, 30), "Click"))
            {
                if (OnClicked != null)
                {
                    OnClicked();
                }
            }
        }
    }
}