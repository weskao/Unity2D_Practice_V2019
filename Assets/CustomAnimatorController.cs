using System;
using ThirdParty.AnimationControl;
using UnityEngine;

public class CustomAnimatorController : MonoBehaviour
{
    private Animator animator;
    private EffectStateProxy _animatorEffectStateProxy;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        
        _animatorEffectStateProxy = animator.GetBehaviour<EffectStateProxy>();
        _animatorEffectStateProxy.OnEffectEnter = OnStateEnter;
        _animatorEffectStateProxy.OnPlayEnd = OnBuildingAnimationPlayDone;
    }

    private void OnStateEnter()
    {
        Debug.LogFormat("<color=yellow>Wes - [CustomAnimatorController] OnStateEnter</color>");
    }

    private void OnIK()
    {
        Debug.LogFormat("<color=yellow>Wes - [CustomAnimatorController] OnIK</color>");
    }

    private void OnBuildingAnimationPlayDone()
    {
        Debug.LogFormat("<color=yellow>Wes - [CustomAnimatorController] OnAnimationPlayDone</color>");
    }

    public void PlayAnimation(string animationName)
    {
        animator.Play(animationName);
    }

    public void GetAnimationLoopTime(string animationName, int layer = 0)
    {
        
        var m_AnimatorClipInfo = animator.GetCurrentAnimatorClipInfo(0);
        //Output the name of the starting clip
        Debug.Log("Starting clip : " + m_AnimatorClipInfo[0].clip);

        // animator.GetCurrentAnimatorStateInfo(layer)
        // m_AnimatorClipInfo[0].
        if (animator.GetCurrentAnimatorStateInfo(layer).IsName(animationName))
        {
            // animator.GetCurrentAnimatorStateInfo(layer).loop
        }
    }
}
