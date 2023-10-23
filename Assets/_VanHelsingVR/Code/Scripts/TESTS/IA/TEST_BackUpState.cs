using UnityEngine;

namespace _VanHelsingVR.TESTS.IA
{
    public class TEST_BackUpState : TEST_State
    {
        private float _secondsForBackup;

        private float _currentSeconds = 0;
        public override void EnterState(TEST_EnemyStateMachine stateMachine)
        {
            _secondsForBackup = stateMachine.SeconsOfBackup;
        }

        public override void UpdateState(TEST_EnemyStateMachine stateMachine)
        {
            _currentSeconds += Time.deltaTime;
            if(_currentSeconds >= _secondsForBackup)
                stateMachine.ChangeState(stateMachine.seekState);
        }

        public override void EndState(TEST_EnemyStateMachine stateMachine)
        {
        
        }
    }
}
