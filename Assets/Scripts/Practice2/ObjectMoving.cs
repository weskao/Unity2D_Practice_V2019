using UnityEngine;

namespace Practice2
{
    public class ObjectMoving : MonoBehaviour
    {
        private static readonly int Forward = Animator.StringToHash("Forward");
        private static readonly int Backward = Animator.StringToHash("Backward");

        private static bool _isForward = true;

        [SerializeField]
        private Animator _animator;

        // [SerializeField]
        // private Button _triggerButton;

        public void MoveSquare()
        {
            _animator.SetTrigger(_isForward ? Forward : Backward);
            _isForward = !_isForward;
        }
    }
}