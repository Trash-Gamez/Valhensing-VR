using _VanHelsingVR.Animation.Gun;
using RacTools.StateMachine;
using UnityEngine;

namespace _VR_Helsing.Gun
{
    public class IdleGunState : BaseGunState
    {
        private Animator _gunAnimator;
        private GunInput _input;
        
        public IdleGunState(GunStateMachine stateMachine, GunInput input) : base(stateMachine)
        {
            _gunAnimator = gunStateMachine.GunAnimator;
            _input = input;
        }

        public override void Enter()
        {
            _gunAnimator.SetBool(GunStateMachine.IsLoading, false);
        }

        public override void CheckState()
        {
            if (!_input.IsTriggering && _input.IsGripping) //ONLY GRIPPING
            { 
                gunStateMachine.ChangeState(gunStateMachine.ActiveGunState);
            }
            else if (_input.IsTriggering && !_input.IsGripping) //ONLY TRIGGERING
            {
                gunStateMachine.ChangeState(gunStateMachine.ShootGunState);
                return;
            }
        }

        public override void Exit()
        {
            
        }
    }
}