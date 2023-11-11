using Sirenix.OdinInspector;
using UnityEngine;

namespace _VanHelsingVR.Enemy
{
    public class FlyingEnemyStateMachine : EnemyStateMachine
    {
        public WayPointManager wayPointManager;
        [Title("States Params")]
        [SerializeField, Min(0.005f)] private float _appearSpeed;
        
        public AppearState AppearState { get; private set; }
        
        protected override void Start()
        {
            AppearState = new AppearState(this, wayPointManager.GetNext(), _appearSpeed);
            ChangeState(AppearState);
        }
    }
}
