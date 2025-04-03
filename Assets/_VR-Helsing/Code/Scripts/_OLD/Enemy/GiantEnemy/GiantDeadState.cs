namespace _VanHelsingVR.Enemy
{
    public class GiantDeadState : BaseEnemyState
    {
        
        public GiantDeadState(GiantEnemyStateMachine stateMachine) : base(stateMachine)
        {
            
        }

        public override void OnStateEnter()
        {
            stateMachine.RestartAnimatorParams();
            stateMachine.Animator.SetBool(GiantEnemyStateMachine.IsDeadAnimID, true);
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