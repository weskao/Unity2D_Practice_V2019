using System;
using UnityEngine;

namespace ThirdParty.AnimationControl
{
    public class AnimationManager
    {
        private Animator _animator;

        private static AnimationManager _animationManager;
        private AnimatorStateInfo _animatorInfo;

        public static AnimationManager Instance
        {
            get
            {
                if (_animationManager == null)
                {
                    _animationManager = new AnimationManager();
                }

                return _animationManager;
            }
        }

        public AnimationManager()
        {
            _animationManager = this;
        }

        public void InitAnimationSettings(Animator animator)
        {
            Debug.LogFormat("<color=yellow>AnimationManager - InitAnimationSettings()</color>");
            _animator = animator;
            _animatorInfo = _animator.GetCurrentAnimatorStateInfo(0);
        }

        public void CheckAnimationCompleted(int animationHash, Action onComplete)
        {
            if (_animator == null)
            {
                Debug.LogWarning("AnimationManager - _animator is null");
                return;
            }

            if (IsAnimationPlayedDone(animationHash))
            {
                onComplete?.Invoke();
                Debug.LogFormat("<color=yellow>AnimationManager - Animation played done!</color>");
            }
        }

        private bool IsAnimationPlayedDone(int animationHash)
        {
            if (_animatorInfo.shortNameHash == animationHash)
            {
                Debug.LogFormat($"<color=Blue>animatorInfo.normalizedTime = {_animatorInfo.normalizedTime}</color>");
            }

            if (_animatorInfo.shortNameHash == animationHash && _animatorInfo.normalizedTime >= 1)
            {
                // Debug.Log($"shortNameHash = {_animator.GetCurrentAnimatorStateInfo(0).shortNameHash}");

                // if (_animatorInfo.normalizedTime < 1)
                // {
                //     return false;
                // }
                // else
                // {
                // Debug.LogFormat($"<color=Blue>★animatorInfo.normalizedTime = {_animatorInfo.normalizedTime}</color>");
                // Debug.Log($"AnimationManager - {animationHash} play done.");
                return true;
                // }
            }

            return false;
        }
    }
}