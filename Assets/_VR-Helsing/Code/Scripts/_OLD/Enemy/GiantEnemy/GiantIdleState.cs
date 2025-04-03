using UnityEngine;

namespace _VanHelsingVR.Enemy
{
    public class GiantIdleState : BaseEnemyState
    {
        private GiantEnemyStateMachine _giantEnemyStateMachine;
        private Transform _target;
        private float _followRadius;

        public GiantIdleState(GiantEnemyStateMachine stateMachine, Transform target, float followRadius) : base(stateMachine)
        {
            _giantEnemyStateMachine = stateMachine;
            _target = target;
            _followRadius = followRadius;
        }

        public override void OnStateEnter()
        {
            
        }

        public override void OnStateUpdate()
        {
            var distance = (_target.position - stateMachine.transform.position).magnitude;
            if(distance <= _followRadius) stateMachine.ChangeState(_giantEnemyStateMachine.FollowingState);
        }

        public override void OnStateFixedUpdate()
        {
            return;
        }

        public override void OnStateExit()
        {
            
        }
    }
}