using ThirdParty.StatusMachine;
using UnityEngine;

namespace Practice2
{
    public class SquareStatusMachineBehaviour : MonoBehaviour
    {
        public void OnForwardStateExit()
        {
            Debug.LogFormat("<color=yellow>Square - OnForwardStateExit()</color>");
        }
    }
}