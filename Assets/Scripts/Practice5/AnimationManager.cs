using UnityEngine;

namespace Practice5
{
    public class AnimationManager : MonoBehaviour
    {
        [SerializeField]
        private LevelUpSequencePanel _levelUpSequencePanel;

        private static int _number = 1;

        private const int ExecutedTime = 2;

        private void Awake()
        {
            for (var i = 1; i <= ExecutedTime; i++)
            {
                _levelUpSequencePanel.Enqueue(new LevelUpRewardItem(), OnCompleteEvent);
            }

            _levelUpSequencePanel.StartCoroutineInQueue();
        }

        private void OnCompleteEvent()
        {
            Debug.Log($"OnComplete! number = {_number}");
            _number++;
        }
    }
}