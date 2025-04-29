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
        
        public ReloadGunState(GunStateMachine stateMachine, Variable<int> magazine) : base(stateMachine)
        {
            _magazine = magazine;
        }

        public override void Enter()
        {
            gunStateMachine.StartCoroutine(ReloadCoroutine());
        }

        public override void Exit()
        {
            
        }
        
        IEnumerator ReloadCoroutine()
        {
            _gunAnimator.Play("Reload");
            if(AudioManager.Instance) AudioManager.Instance.PlaySound2D("Reload");
            yield return new WaitForSeconds(gunStateMachine.Config.ReloadTime);
            Reload();
        }
   
        private void Reload()
        {
            _magazine.Value += gunStateMachine.Config.ReloadBullets;
            if (_magazine.Value >= gunStateMachine.Config.MagazineSize)
            {
                if(AudioManager.Instance) AudioManager.Instance.PlaySound2D("FullReload");
                _magazine.Value = gunStateMachine.Config.MagazineSize;
            }
        }
    }
}