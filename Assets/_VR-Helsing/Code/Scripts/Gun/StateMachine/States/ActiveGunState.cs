using System.Linq;
using _VanHelsingVR.Animation.Gun;
using _VR_Helsing.Utils;
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

        private Vector3 _localAngularVel;
        
        public ActiveGunState(GunStateMachine stateMachine, GunInput input) : base(stateMachine)
        {
            _input = input;
            _gunAnimator = gunStateMachine.GunAnimator; 
            _gunRigidBody = gunStateMachine.GunRigidbody;

            _zAngularVelocityThreshold = gunStateMachine.ZAngularVelocityThreshold;
            _yVelocityThreshold = gunStateMachine.YVelocityThreshold;
            _xAngularVelocityThreshold = gunStateMachine.XAngularVelocityThreshold;
        }

        public override void Enter()
        {
            _gunAnimator.SetBool(GunStateMachine.IsLoading, true);
            
            #if UNITY_EDITOR //Si estas en editor, actualiza en cada entrada el valor, por temas de debugging
            _zAngularVelocityThreshold = gunStateMachine.ZAngularVelocityThreshold;
            _yVelocityThreshold = gunStateMachine.YVelocityThreshold;
            _xAngularVelocityThreshold = gunStateMachine.XAngularVelocityThreshold;
            #endif
        }

        public override void FixedUpdate() => GetLocalAngularVelocities();

        public override void Exit()
        { 
            
        }

        public override void CheckState()
        {
         
            Vector3 localVelocity = _gunRigidBody.transform.InverseTransformDirection(_gunRigidBody.linearVelocity);
            if (Mathf.Abs(_gunRigidBody.veloci) > _yVelocityThreshold && _input.IsGripping)
            {
                gunStateMachine.ChangeState(gunStateMachine.ReloadGunState);
                return;
            }
        
            if (Mathf.Abs(_localAngularVel.z) > _zAngularVelocityThreshold && _input.IsGripping)
            {
                gunStateMachine.ChangeState(gunStateMachine.ChangeWeaponGunState);
                return;
            }
        }


        private void GetLocalAngularVelocities()
        {
            _localAngularVel = _gunRigidBody.GetLocalAngularVelocity();
        }
    }
}