using UnityEngine;

namespace _VanHelsingVR.Enemy
{
    public class RunAwayParams
    {
        public float RunAwayCircle { get; set; }
        public float SafeRadius { get; set; }
        public Transform Target { get; set; }
        public float Speed { get; set; }
    }
}