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
        public string id;
    }

    [Serializable]
    public class MyGroup
    {
        public Button[] buttons;
        public Transform transform;
        public int number;

        [SerializeField]
        private Profile _profile;
    }

    public class ObjectMoving : MonoBehaviour
    {
        private static readonly int Forward = Animator.StringToHash("Forward");
        private static readonly int Backward = Animator.StringToHash("Backward");

        public GameObject gameObject;

        private static bool _isForward = true;

        [SerializeField]
        private Animator _animator;

        public Vector3 transformPosition;

        [SerializeField]
        private MyGroup _myGroup;

        // [SerializeField]
        // private Button _triggerButton;

        public void MoveSquare()
        {
            transformPosition = gameObject.transform.position;

            _animator.SetTrigger(_isForward ? Forward : Backward);
            _isForward = !_isForward;
        }
    }
}