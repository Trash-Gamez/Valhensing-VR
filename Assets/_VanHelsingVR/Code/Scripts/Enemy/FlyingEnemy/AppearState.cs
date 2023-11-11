using UnityEngine;

namespace _VanHelsingVR.Enemy
{
    public class AppearState : BaseEnemyState
    {
        private Transform _appearPos;
        private Vector3 target;
        private float speed;
        
        public AppearState(FlyingEnemyStateMachine stateMachine, Transform appearPos, float speed) : base(stateMachine)
        {
            _appearPos = appearPos;
        }
        
        public override void OnStateEnter()
        {
            target = stateMachine.transform.InverseTransformPoint(_appearPos.position);
        }

        public override void OnStateUpdate()
        {
            stateMachine.transform.position 
                = Vector3.MoveTowards(stateMachine.transform.position, target, Time.deltaTime * speed);

            if ((stateMachine.transform.position - target).sqrMagnitude > 0.0005f)
            {
                stateMachine.ChangeState(stateMachine.NoneState);
            }
        }

        public override void OnStateFixedUpdate()
        {
            return;
        }

        public override void OnStateExit()
        {
            Debug.Log("Sali del appear");
        }
    }
}