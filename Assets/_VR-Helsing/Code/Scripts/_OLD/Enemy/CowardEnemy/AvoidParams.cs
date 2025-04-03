using _VanHelsingVR.IA;
using RacTools.RuntimeSet;

namespace _VanHelsingVR.Enemy
{
    public struct AvoidParams
    {
        public RuntimeSet<IAObstacle> Obstacles { get; set; }
        public float MaxSeeAhead { get; set; }
        public float MaxAvoidForce { get; set; }
    }
}