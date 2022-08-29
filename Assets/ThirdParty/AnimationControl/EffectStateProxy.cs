using System;
using UnityEngine;

namespace ThirdParty.AnimationControl
{
    public class EffectStateProxy : StateMachineBehaviour
    {
        public Action OnEffectEnter;
        public Action OnPlayEnd;

        [SerializeField]
        private string _targetState = string.Empty;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (stateInfo.IsName(_targetState))
            {
                OnEffectEnter?.Invoke();
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (stateInfo.IsName(_targetState))
            {
                OnPlayEnd?.Invoke();
            }
        }
    }
}