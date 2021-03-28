using UnityEngine;

namespace ThirdParty.StatusMachine
{
    // Code 來源: https://www.zhihu.com/question/40492181/answer/506143809
    public abstract class LogicalStateMachineBehaviour : StateMachineBehaviour
    {
        // PRAGMA MARK - StateMachineBehaviour Lifecycle
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            this.animator = animator;
            this.stateInfo = stateInfo;

            // You can reference other monobehaviours here
            if (this.animator != null)
            {
                // this.creature = animator.gameObject.GetComponent<Creature>();
            }

            this.OnStateEntered();
            this._active = true;
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            this.animator = animator;
            this.stateInfo = stateInfo;

            this._active = false;
            this.OnStateExited();
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            switch (animator.updateMode)
            {
                case AnimatorUpdateMode.AnimatePhysics:
                    this.deltaTime = Time.fixedDeltaTime;
                    break;

                case AnimatorUpdateMode.Normal:
                    this.deltaTime = Time.deltaTime;
                    break;

                case AnimatorUpdateMode.UnscaledTime:
                    this.deltaTime = Time.unscaledDeltaTime;
                    break;

                default:
                    this.deltaTime = 0f;
                    break;
            }
            this.stateInfo = stateInfo;
            this.OnStateUpdated();
        }

        // PRAGMA MARK - Internal
        private bool _active = false;

        protected Animator animator { get; private set; }
        protected float deltaTime { get; private set; }

        // TODO: You should store reference to the script that controls what this agent could do
        //       along with any other important information.

        // protected Creature creature { get; private set; }

        protected AnimatorStateInfo stateInfo;

        private void OnDisable()
        {
            if (this._active)
            {
                this.OnStateExited();
            }
        }

        // Implement the following functions to write actual state behaviors
        protected virtual void OnStateEntered() { }

        protected virtual void OnStateExited()
        {
        }

        protected virtual void OnStateUpdated()
        {
        }
    }
}