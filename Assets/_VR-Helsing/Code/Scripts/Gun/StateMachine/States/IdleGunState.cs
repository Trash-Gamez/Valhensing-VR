using _VanHelsingVR.Animation.Gun;
using RacTools.StateMachine;
using UnityEngine;

namespace _VR_Helsing.Gun
{
    public class IdleGunState : BaseGunState
    {
        private Animator _gunAnimator;
        private GunInput _input;
        
        public IdleGunState(GunStateMachine stateMachine, GunStateFactory factory) : base(stateMachine, factory)
        {
            _gunAnimator = GunStateMachine.GunAnimator;
            _input = GunStateMachine.Input;
        }

        public override void Enter()
        {
            _gunAnimator.SetBool(GunStateMachine.IsLoading, false);
        }

        public override void CheckState()
        {
            if (!_input.IsTriggering && _input.IsGripping) //ONLY GRIPPING
            { 
                GunStateMachine.ChangeState(StateFactory.ActiveState);
            }
            else if (_input.IsTriggering && !_input.IsGripping) //ONLY TRIGGERING
            {
                GunStateMachine.ChangeState(StateFactory.ShootState);
                return;
            }
        }

        public override void Exit()
        {
            
        }
    }
}