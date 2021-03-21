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

        private void Update()
        {
            // if (_gameObject.activeInHierarchy)
            // {
            //     if (IsAnimationPlayDone(Forward))
            //     {
            //         Debug.Log($"Forward play done.");
            //     }
            //     else
            //     {
            //         Debug.LogFormat("<color=green>Forward not play done.</color>");
            //     }
            // }
        }

        private bool IsAnimationPlayDone(int targetHash)
        {
            var animatorInfo = _animator.GetCurrentAnimatorStateInfo(0);
            if (animatorInfo.shortNameHash == targetHash)
            {
                Debug.LogFormat($"<color=Blue>animatorInfo.normalizedTime = {animatorInfo.normalizedTime}</color>");

                if (animatorInfo.normalizedTime >= 1.0f)
                {
                    // Debug.Log($"{targetHash} play done.");
                    return true;
                }
                else
                {
                    // Debug.Log($"{targetHash} not play done.");
                    return false;
                }
            }

            return false;
        }
    }
}