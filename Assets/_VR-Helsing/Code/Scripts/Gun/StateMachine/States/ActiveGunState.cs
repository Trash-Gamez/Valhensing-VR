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
        
        private float _zAngularVelocityThreshold;
        private float _yVelocityThreshold;
        private float _xAngularVelocityThreshold;

        
        public ActiveGunState(GunStateMachine stateMachine, GunStateFactory factory) : base(stateMachine, factory)
        {
            _input = GunStateMachine.Input;
            _gunAnimator = GunStateMachine.GunAnimator; 
            _gunRigidBody = GunStateMachine.GunRigidbody;

            _zAngularVelocityThreshold = GunStateMachine.ZAngularVelocityThreshold;
            _yVelocityThreshold = GunStateMachine.YVelocityThreshold;
            _xAngularVelocityThreshold = GunStateMachine.XAngularVelocityThreshold;
        }

        public override void Enter()
        {
            _gunAnimator.SetBool(GunStateMachine.IsLoading, true);
            
            #if UNITY_EDITOR //Si estas en editor, actualiza en cada entrada el valor, por temas de debugging
            _zAngularVelocityThreshold = GunStateMachine.ZAngularVelocityThreshold;
            _yVelocityThreshold = GunStateMachine.YVelocityThreshold;
            _xAngularVelocityThreshold = GunStateMachine.XAngularVelocityThreshold;
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

            //Primero checa si cambio de arma
            var localZAngVel = GunStateMachine.ZLocalAngVelBuffer.Average();
        
            if (localZAngVel.IsPassedThreshold(_zAngularVelocityThreshold))
            {
                GunStateMachine.ChangeState(StateFactory.ChangeWeaponState);
                return;
            }
            
            //segundo checa la recarga
            var localYVelocity = GunStateMachine.YLocalVelBuffer.Average();
            var localXAngVel = GunStateMachine.XLocalAngVelBuffer.Average();
            
            if (localYVelocity.IsPassedThreshold(_yVelocityThreshold) 
                && localXAngVel.IsPassedThreshold(_xAngularVelocityThreshold))
            {
                GunStateMachine.ChangeState(StateFactory.ReloadState);
                return;
            }
            
        }
    }
}