namespace _VanHelsingVR.Enemy
{
    public abstract class BaseEnemyState
    {
        public EnemyStateMachine StateMachine => stateMachine;
        protected EnemyStateMachine stateMachine;

        public BaseEnemyState(EnemyStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }
        
        public abstract void OnStateEnter();

        public abstract void OnStateUpdate();

        public abstract void OnStateFixedUpdate();

        public abstract void OnStateExit();
    }
}