using UnityEngine;

namespace Practice2
{
    public class Square : MonoBehaviour
    {
        [SerializeField]
        private CustomAnimatorController customAnimatorController = null;

        [SerializeField]
        private string toggleVisibleAnimationName = "Square_ToggleVisible";

        private void Update()
        {
            if (Input.GetKey(KeyCode.S))
            {
                customAnimatorController.PlayAnimation(toggleVisibleAnimationName);
            }

            if (Input.GetKey(KeyCode.G))
            {
                Debug.LogFormat("<color=yellow>Wes - [Square] Start ot get information</color>");
                customAnimatorController.GetAnimationLoopTime(toggleVisibleAnimationName);
            }
        }
    }
}