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
        
        public FlyAroundState(FlyingEnemyStateMachine stateMachine, Range attackRange) : base(stateMachine)
        {
            _flyingEnemyStateMachine = stateMachine;
            _attackRange = attackRange;
        }

        private void SelectNewTarget()
        {
            _target = _flyingEnemyStateMachine.wayPointManager.GetNext();
        }

        private IEnumerator AttackWaitCor()
        {
            yield return new WaitForSeconds(Random.Range(_attackRange.Min, _attackRange.Max));
            
        }

        public override void OnStateEnter()
        {
            SelectNewTarget();
        }

        public override void OnStateUpdate()
        {
            var targetPos = _target.position;
            var enemyPos = stateMachine.transform.position;
            if((targetPos - enemyPos).sqrMagnitude < 0.005f)
                SelectNewTarget();
            
        }

        public override void OnStateFixedUpdate()
        {
            throw new System.NotImplementedException();
        }

        public override void OnStateExit()
        {
            throw new System.NotImplementedException();
        }
    }
}