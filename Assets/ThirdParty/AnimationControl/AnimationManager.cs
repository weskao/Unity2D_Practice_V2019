using System;
using UnityEngine;

namespace ThirdParty.AnimationControl
{
    public class AnimationManager
    {
        private Animator _animator;

        private bool _isAnimationEverPlayed = false;

        private static AnimationManager _animationManager;

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
            _animator = animator;
            _isAnimationEverPlayed = false;
        }

        public void CheckAnimationCompleted(int animationHash, Action onComplete)
        {
            if (_animator == null)
            {
                Debug.LogWarning("AnimationManager - _animator is null");
                return;
            }

            if (IsAnimationPlayedDone(animationHash) && !_isAnimationEverPlayed)
            {
                _isAnimationEverPlayed = true;
                onComplete?.Invoke();
                Debug.LogFormat("<color=yellow>Wes - AnimationManager - Animation played done!</color>");
            }
        }

        private bool IsAnimationPlayedDone(int animationHash)
        {
            var animatorInfo = _animator.GetCurrentAnimatorStateInfo(0);
            if (animatorInfo.shortNameHash == animationHash)
            {
                Debug.Log($"Wes - shortNameHash = {_animator.GetCurrentAnimatorStateInfo(0).shortNameHash}");
                Debug.LogFormat($"<color=Blue>animatorInfo.normalizedTime = {animatorInfo.normalizedTime}</color>");

                if (animatorInfo.normalizedTime % 1 < 0.99f)
                {
                    return false;
                }
                else
                {
                    Debug.LogFormat($"<color=Blue>★animatorInfo.normalizedTime = {animatorInfo.normalizedTime}</color>");
                    Debug.LogFormat("<color=yellow>Wes - AnimationManager - PlayDone!!!</color>");
                }

                if (animatorInfo.normalizedTime >= 1)
                {
                    Debug.Log($"{animationHash} play done.");
                    return true;
                }
                else
                {
                    // Debug.Log($"{animationHash} not play done.");
                    return false;
                }
            }

            return false;
        }
    }
}