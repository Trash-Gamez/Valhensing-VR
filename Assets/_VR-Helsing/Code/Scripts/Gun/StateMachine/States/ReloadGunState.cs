using System.Collections;
using RacTools.StateMachine;
using RacTools.Variables;
using UnityEngine;

namespace _VR_Helsing.Gun
{
    public class ReloadGunState : BaseGunState
    {
        private Variable<int> _magazine;
        private Animator _gunAnimator;

        private bool _ended;
        
        public ReloadGunState(GunStateMachine stateMachine, GunStateFactory factory) : base(stateMachine, factory)
        {
            _gunAnimator = GunStateMachine.GunAnimator;
            _magazine = GunStateMachine.Magazine;
        }

        public override void Enter()
        {
            _ended = false;
            GunStateMachine.StartCoroutine(ReloadCoroutine());
        }

        public override void Exit()
        {
            
        }

        public override void CheckState()
        {
            if (!_ended) return;
            
            GunStateMachine.ChangeState(StateFactory.ActiveState);
        }

        IEnumerator ReloadCoroutine()
        {
            _gunAnimator.Play("Reload");
            if(AudioManager.Instance) AudioManager.Instance.PlaySound2D("Reload");
            yield return new WaitForSeconds(GunStateMachine.Config.ReloadTime);
            Reload();
            _ended = true;
        }
   
        private void Reload()
        {
            _magazine.Value += GunStateMachine.Config.ReloadBullets;
            if (_magazine.Value >= GunStateMachine.Config.MagazineSize)
            {
                if(AudioManager.Instance) AudioManager.Instance.PlaySound2D("FullReload");
                _magazine.Value = GunStateMachine.Config.MagazineSize;
            }
        }
    }
}