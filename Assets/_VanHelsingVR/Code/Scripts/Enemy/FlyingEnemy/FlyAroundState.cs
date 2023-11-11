using System.Collections;
using _VanHelsingVR.Utilities;
using UnityEngine;
using Zenject;

namespace _VanHelsingVR.Enemy
{
    public class FlyAroundState : BaseEnemyState
    {
        private FlyingEnemyStateMachine _flyingEnemyStateMachine;
        private Transform _target;
        private Range _attackRange;
        private float _flySpeed;
        
        public FlyAroundState(FlyingEnemyStateMachine stateMachine, Range attackRange, float flySpeed) : base(stateMachine)
        {
            _flyingEnemyStateMachine = stateMachine;
            _attackRange = attackRange;
            _flySpeed = flySpeed;
        }

        private void SelectNewTarget()
        {
            _target = _flyingEnemyStateMachine.wayPointManager.GetNext();
        }

        private IEnumerator AttackWaitCor()
        {
            yield return new WaitForSeconds(Random.Range(_attackRange.Min, _attackRange.Max));
            stateMachine.ChangeState(_flyingEnemyStateMachine.FlyingAttackState);
        }

        public override void OnStateEnter()
        {
            _flyingEnemyStateMachine.RestartAnimatorParams();
            _flyingEnemyStateMachine.Animator.SetBool(FlyingEnemyStateMachine.FlyAnimationID, true);
            stateMachine.StartCoroutine(AttackWaitCor());
            SelectNewTarget();
        }

        public override void OnStateUpdate()
        {
            var targetPos = _target.position;
            var enemyPos = stateMachine.transform.position;

            stateMachine.transform.position = Vector3.MoveTowards(enemyPos, targetPos, Time.deltaTime * _flySpeed);
            stateMachine.transform.LookAt(_target);
            
            if((targetPos - enemyPos).sqrMagnitude < 0.005f)
                SelectNewTarget();
        }

        public override void OnStateFixedUpdate()
        {
            return;
        }

        public override void OnStateExit()
        {
            _flyingEnemyStateMachine.wayPointManager.Restart();
        }
    }
}