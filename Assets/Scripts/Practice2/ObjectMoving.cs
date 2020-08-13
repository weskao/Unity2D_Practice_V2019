using System;
using UnityEngine;
using UnityEngine.UI;

namespace Practice2
{
    [Serializable]
    public class Profile
    {
        public Image[] images;
        public Text text;
        public int id;
    }

    public class GroupA : MonoBehaviour
    {
        private GameObject _gameObject2;

        private bool _isForward2 = true;
    }

    public class ObjectMoving : MonoBehaviour
    {
        private static readonly int Forward = Animator.StringToHash("Forward");
        private static readonly int Backward = Animator.StringToHash("Backward");

        public Profile profile;

        [SerializeField]
        private GroupA _groupA;

        [SerializeField]
        private GameObject _gameObject;

        private static bool _isForward = true;

        [SerializeField]
        private Animator _animator;

        public Vector3 transformPosition;

        [SerializeField]
        private MyGroup _myGroup;

        // [SerializeField]
        // private Button _triggerButton;

        private void Awake()
        {
            if (_myGroup != null)
            {
                Debug.Log($"number = {_myGroup.number}");
            }
        }

        public void MoveSquare()
        {
            transformPosition = _gameObject.transform.position;

            _animator.SetTrigger(_isForward ? Forward : Backward);
            _isForward = !_isForward;
        }
    }
}