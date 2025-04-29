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
        
        public ReloadGunState(GunStateMachine stateMachine, GunStateFactory factory) : base(stateMachine, factory)
        {
            _magazine = GunStateMachine.Magazine;
        }

        public override void Enter()
        {
            GunStateMachine.StartCoroutine(ReloadCoroutine());
        }

        public override void Exit()
        {
            
        }
        
        IEnumerator ReloadCoroutine()
        {
            _gunAnimator.Play("Reload");
            if(AudioManager.Instance) AudioManager.Instance.PlaySound2D("Reload");
            yield return new WaitForSeconds(GunStateMachine.Config.ReloadTime);
            Reload();
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