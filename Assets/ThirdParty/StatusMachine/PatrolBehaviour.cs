using UnityEngine;

namespace ThirdParty.StatusMachine
{
    // Code 來源: https://www.zhihu.com/question/40492181/answer/506143809
    /// <summary>
    /// An Example Patrol behaviour of Mechanim State Machine AI.
    /// At this state, the agent will get an random destination and try to move towards it.
    /// This script only updates necessary parameters for the mechanim state transition to work.
    /// The end condition of the state should be determined from the Mechanim State Machine editor.
    /// </summary>
    public class PatrolBehaviour : LogicalStateMachineBehaviour
    {
        public float StateDurationMin = 3.0f;
        public float StateDurationMax = 6.0f;
        public float newDestRadius = 10.0f;
        public Vector3 destination { get; protected set; }

        protected override void OnStateEntered()
        {
            this.animator.SetFloat("StateDuration", Random.Range(StateDurationMin, StateDurationMax));
            GetRandomDestination();
        }

        protected override void OnStateUpdated()
        {
            // Update Parameters
            this.animator.SetFloat("StateDuration", this.animator.GetFloat("StateDuration") - this.deltaTime);
            // this.animator.SetFloat("DistanceAway", Vector3.Distance(destination, creature.transform.position));

            // Debug.DrawLine(creature.transform.position, destination, Color.yellow);
            // creature.MoveTowards(destination, Time.fixedDeltaTime); // Replace with your movement function
        }

        protected override void OnStateExited()
        {
            this.animator.SetFloat("StateDuration", 0f);
        }

        private void GetRandomDestination()
        {
            // destination = creature.transform.position + Random.onUnitSphere * newDestRadius;
        }
    }
}