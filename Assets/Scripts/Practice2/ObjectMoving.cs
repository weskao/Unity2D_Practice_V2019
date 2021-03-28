using System;
using ThirdParty.AnimationControl;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

namespace Practice2
{
    [Serializable]
    public class Profile : ScriptableObject
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

        [SerializeField]
        private AnimatorStateMachine _originalAnimatorStateMachine;

        [SerializeField]
        private Profile _profile;

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

            Debug.Log($"Wes - Forward = {Forward}");
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

        private void OnAnimationComplete()
        {
            Debug.LogFormat("<color=yellow>OnAnimationComplete()</color>");

            _animator.SetTrigger(Backward);
        }
    }
}