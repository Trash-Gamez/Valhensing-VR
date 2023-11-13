namespace _VanHelsingVR.Enemy
{
    public class FlyingDeadState : BaseEnemyState
    {
        private FlyingEnemyStateMachine _flyingEnemyStateMachine;
        public FlyingDeadState(FlyingEnemyStateMachine stateMachine) : base(stateMachine)
        {
            _flyingEnemyStateMachine = stateMachine;
        }

        public override void OnStateEnter()
        {
            _flyingEnemyStateMachine.wayPointManager.Restart();
            _flyingEnemyStateMachine.RestartAnimatorParams();
            stateMachine.Animator.SetBool(FlyingEnemyStateMachine.DeadAnimationID, true);
            UnityEngine.Debug.Log("Iniciar Estado de muerte");
        }

        public override void OnStateUpdate()
        {
            return;
        }

        public override void OnStateFixedUpdate()
        {
            return;
        }

        public override void OnStateExit()
        {
            return;
        }
    }
}