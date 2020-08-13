using UnityEngine;
using UnityEngine.UI;

namespace Practice2
{
    public class MyGroup : MonoBehaviour
    {
        public Button[] buttons;
        public Transform transform;
        public int number;

        [SerializeField]
        private Profile _profile;
    }
}