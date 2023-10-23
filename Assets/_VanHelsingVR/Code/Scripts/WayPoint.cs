using UnityEngine;

namespace _VanHelsingVR
{
    public class WayPoint : MonoBehaviour
    {
        public float Time;
        public bool canContinue;
        public bool canRotate=true;

        public void Continue()
        {
            canContinue = true;
        }
    }
}
