using UnityEngine;

namespace _VanHelsingVR.Enemy
{
    public class AppearState : BaseEnemyState
    {
        private FlyingEnemyStateMachine _flyingEnemyStateMachine;
        private Transform _appearPos;
        private Vector3 _target;
        private float _speed;
        
        public AppearState(FlyingEnemyStateMachine stateMachine, Transform appearPos, float speed) : base(stateMachine)
        {
            _appearPos = appearPos;
            _flyingEnemyStateMachine = stateMachine;
            _speed = speed;
        }
        
        public override void OnStateEnter()
        {
            //target = stateMachine.transform.InverseTransformPoint(_appearPos.position);
            _target = _appearPos.position;
        }

        public override void OnStateUpdate()
        {
            stateMachine.transform.position 
                = Vector3.MoveTowards(stateMachine.transform.position, _target, Time.deltaTime * _speed);

            if ((stateMachine.transform.position - _target).sqrMagnitude < 0.0005f)
            {
                stateMachine.ChangeState(_flyingEnemyStateMachine.FlyAroundState);
            }
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