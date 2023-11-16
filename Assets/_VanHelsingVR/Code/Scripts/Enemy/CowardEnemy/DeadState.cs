namespace _VanHelsingVR.Enemy
{
    public class DeadState : BaseEnemyState
    {
        public DeadState(CowardEnemyStateMachine stateMachine) : base(stateMachine){}

        public override void OnStateEnter()
        {
            stateMachine.RestartAnimatorParams();
            stateMachine.Animator.SetBool(CowardEnemyStateMachine.IsDeadAnimID, true);
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