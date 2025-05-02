using UnityEngine;

namespace _VR_Helsing.Gun
{
    /// <summary>
    /// Estado en el que se encuentra el arma, tras presionar "grip"
    /// Este estado mide si se debe recargar o no
    /// </summary>
    public class ActiveGunState : BaseGunState
    {
        private GunInput _input;
        
        private Rigidbody _gunRigidBody;
        private Animator _gunAnimator;

        private float _yVelocityThreshold;
        
        public ActiveGunState(GunStateMachine stateMachine, GunStateFactory factory) : base(stateMachine, factory)
        {
            _input = GunStateMachine.Input;
            _gunAnimator = GunStateMachine.GunAnimator; 
            _gunRigidBody = GunStateMachine.GunRigidbody;

            _yVelocityThreshold = GunStateMachine.YVelocityThreshold;
        }

        public override void Enter()
        {
            _gunAnimator.SetBool(GunStateMachine.IsLoading, true);
            
            #if UNITY_EDITOR //Si estas en editor, actualiza en cada entrada el valor, por temas de debugging
            _yVelocityThreshold = GunStateMachine.YVelocityThreshold;
            #endif
        }

        public override void Exit()
        { 
            
        }

        public override void CheckState()
        {
            if (!_input.IsGripping)
            {
                GunStateMachine.ChangeState(StateFactory.IdleState);
                return;
            }

            
            var speedY = _gunRigidBody.angularVelocity.x + _gunRigidBody.linearVelocity.y; //Hacer queel angular sea más importante
      
            if (Mathf.Abs(speedY) > _yVelocityThreshold)
            {
                GunStateMachine.ChangeState(StateFactory.ReloadState);
                return;
            }
        }
    }
}