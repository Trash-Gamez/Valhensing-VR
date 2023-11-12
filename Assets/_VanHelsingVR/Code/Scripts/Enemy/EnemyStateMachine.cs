using System;
using UnityEngine;

namespace _VanHelsingVR.Enemy
{
    public abstract class EnemyStateMachine : MonoBehaviour
    {
        [field: SerializeField] public Animator Animator { get; private set; }
        
        public BaseEnemyState CurrentState { get; private set; }
        public NoneState NoneState { get; private set; }
        
        public event Action<BaseEnemyState> OnChangeState = delegate {  };

        public abstract void RestartAnimatorParams();

        protected virtual void Start()
        {
            NoneState = new NoneState(this);
        }

        public void ChangeState(BaseEnemyState newEnemyState)
        {
            if(CurrentState != null)
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