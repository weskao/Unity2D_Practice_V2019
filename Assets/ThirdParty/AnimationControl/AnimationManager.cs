using System;
using UnityEngine;

namespace ThirdParty.AnimationControl
{
    public class AnimationManager
    {
        private Animator _animator;

        private static AnimationManager _animationManager;
        private bool _isAnimationPlayedDone = false; // 即時判斷動畫是否執行完畢, 避免 onComplete 短時間內被重複呼叫

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
        }

        public void CheckAnimationCompleted(int animationHash, Action onComplete)
        {
            if (_animator == null)
            {
                Debug.LogWarning("AnimationManager - _animator is null");
                return;
            }

            if (_isAnimationPlayedDone)
            {
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
            var animatorInfo = _animator.GetCurrentAnimatorStateInfo(0);

            if (animatorInfo.shortNameHash == animationHash && animatorInfo.normalizedTime >= 1)
            {
                _isAnimationPlayedDone = true;
                Debug.LogFormat($"<color=Blue>animatorInfo.normalizedTime = {animatorInfo.normalizedTime}</color>");

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