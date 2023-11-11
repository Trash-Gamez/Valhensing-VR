using UnityEngine;

namespace _VanHelsingVR.Enemy
{
    public class NoneState : BaseEnemyState
    {
        public NoneState(EnemyStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void OnStateEnter()
        {
            Debug.Log("Entre en nadqa");
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