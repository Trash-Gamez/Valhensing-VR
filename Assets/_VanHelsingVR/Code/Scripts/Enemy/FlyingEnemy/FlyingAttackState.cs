using System.Collections;
using UnityEngine;

namespace _VanHelsingVR.Enemy
{
    public class FlyingAttackState : BaseEnemyState
    {
        private FlyingEnemyStateMachine _flyingEnemyStateMachine;
        public FlyingAttackState(FlyingEnemyStateMachine stateMachine) : base(stateMachine)
        {
            _flyingEnemyStateMachine = stateMachine;
        }

        public override void OnStateEnter()
        {
            _flyingEnemyStateMachine.RestartAnimatorParams();
            _flyingEnemyStateMachine.Animator.SetBool(FlyingEnemyStateMachine.AttackAnimationID, true);
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