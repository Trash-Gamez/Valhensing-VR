using UnityEngine;

namespace _VanHelsingVR.Enemy
{
    public class GiantAttackingEnemyState : BaseEnemyState
    {
        private GiantEnemyStateMachine _giantEnemyStateMachine;
        public GiantAttackingEnemyState(GiantEnemyStateMachine stateMachine) : base(stateMachine)
        {
            _giantEnemyStateMachine = stateMachine;
        }

        public override void OnStateEnter()
        {
            Debug.Log("Atacando");
            stateMachine.RestartAnimatorParams();
            stateMachine.Animator.SetBool(GiantEnemyStateMachine.IsAttackingAnimID, true);
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