using System;
using UnityEngine;

namespace _VanHelsingVR.Enemy
{
    public abstract class EnemyStateMachine : MonoBehaviour
    {
        public BaseEnemyState CurrentState { get; private set; }
        public event Action<BaseEnemyState> OnChangeState = delegate(BaseEnemyState state) {  };
        
        public NoneState NoneState { get; private set; }

        protected virtual void Start()
        {
            NoneState = new NoneState(this);
        }

        public void ChangeState(BaseEnemyState newEnemyState)
        {
            CurrentState.OnStateExit();
            CurrentState = newEnemyState;
            CurrentState.OnStateEnter();
            OnChangeState?.Invoke(CurrentState);
        }

        protected virtual void Update()
        {
            CurrentState.OnStateUpdate();
        }

        protected virtual void FixedUpdate()
        {
            CurrentState.OnStateFixedUpdate();
        }
    }
}