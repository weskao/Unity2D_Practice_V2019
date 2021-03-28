using System;
using UnityEngine;

namespace ThirdParty.StatusMachine
{
    public abstract class CustomStateMachineBehaviour : MonoBehaviour
    {
        public Action onStateEnter;
        public Action onStateUpdate;
        public Action onStateExit;
        public Action onStateMove;
        public Action onStateIK;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public virtual void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        public virtual void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        public virtual void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Debug.LogFormat("<color=yellow>AnimationStatusChecker - OnStateExit()</color>");
            Debug.Log($"stateInfo.shortNameHash = {stateInfo.shortNameHash}");
            Debug.Log($"stateInfo.normalizedTime = {stateInfo.normalizedTime}");
            onStateExit?.Invoke();
            Debug.LogFormat("<color=yellow>AnimationStatusChecker - ===========</color>");
        }

        // OnStateMove is called right after Animator.OnAnimatorMove()
        public virtual void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            // Implement code that processes and affects root motion
        }

        // OnStateIK is called right after Animator.OnAnimatorIK()
        public virtual void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            // Implement code that sets up animation IK (inverse kinematics)
        }
    }
}