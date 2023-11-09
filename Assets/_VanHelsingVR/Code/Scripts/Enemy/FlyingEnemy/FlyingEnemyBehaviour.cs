namespace _VanHelsingVR.Enemy
{
    public class FlyingEnemyBehaviour : EnemyStateMachine
    {
        public WayPointManager wayPointManager;

        public AppearState AppearState { get; private set; }
        
        protected override void Start()
        {
            wayPointManager.GetNext();
        }
    }
}
