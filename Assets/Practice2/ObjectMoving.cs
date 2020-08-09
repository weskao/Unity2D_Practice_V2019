using UnityEngine;
using UnityEngine.Experimental.UIElements;

namespace Practice2
{
    public class ObjectMoving : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private Button _triggerButton;

        private static readonly int Forward = Animator.StringToHash("Forward");

        private void Awake()
        {
        }

        public void MoveSquare()
        {
            _animator.SetTrigger(Forward);
        }
    }
}