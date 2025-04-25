using System;
using UnityEngine;

namespace RacTools.StateMachine
{
    public abstract class BaseStateMachine : MonoBehaviour
    {
        protected BaseState currentState;

        protected virtual void Update()
        {
            currentState.Update();
        }

        protected virtual void FixedUpdate()
        {
            currentState.FixedUpdate();
        }

        public virtual void ChangeState(BaseState state)
        {
            currentState.Exit();
            currentState = state;
            currentState.Enter();
        }
    }
}
