using UnityEngine;
using UnityEngine.Events;

public class AnimationStatusMachine : StateMachineBehaviour
{
    [SerializeField]
    private UnityEvent _onStateExit;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.LogFormat("<color=yellow>AnimationStatusChecker - OnStateExit()</color>");
        Debug.Log($"stateInfo.shortNameHash = {stateInfo.shortNameHash}");
        Debug.Log($"stateInfo.normalizedTime = {stateInfo.normalizedTime}");
        _onStateExit?.Invoke();
        Debug.LogFormat("<color=yellow>AnimationStatusChecker - ===========</color>");
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Implement code that processes and affects root motion
    }

    // OnStateIK is called right after Animator.OnAnimatorIK()
    public override void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Implement code that sets up animation IK (inverse kinematics)
    }
}