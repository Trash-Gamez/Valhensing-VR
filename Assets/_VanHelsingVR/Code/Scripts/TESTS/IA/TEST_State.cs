namespace _VanHelsingVR.TESTS.IA
{
    public abstract class TEST_State
    {
        public abstract void EnterState(TEST_EnemyStateMachine stateMachine);

        public abstract void UpdateState(TEST_EnemyStateMachine stateMachine);

        public abstract void EndState(TEST_EnemyStateMachine stateMachine);
    }
}
