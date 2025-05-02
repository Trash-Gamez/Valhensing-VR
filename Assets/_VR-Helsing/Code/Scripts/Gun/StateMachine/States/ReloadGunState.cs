using System.Collections;
using UnityEngine;

using _VR_Helsing.Gun.Animation;
using RacTools.Variables;

namespace _VR_Helsing.Gun
{
    public class ReloadGunState : BaseGunState
    {
        private static readonly int ReloadSpeedMultiplier = Animator.StringToHash("ReloadSpeedMultiplier");
        private Variable<int> _magazine;
        private Animator _gunAnimator;
        private readonly AnimationClip _reloadClip;
        
        private float _reloadClipLenght;
        private bool _ended;
        
        public ReloadGunState(GunStateMachine stateMachine, GunStateFactory factory) : base(stateMachine, factory)
        {
            _gunAnimator = GunStateMachine.GunAnimator;
            
            _reloadClip = GunStateMachine.ReloadClip;
            _magazine = GunStateMachine.Magazine;
        }

        public override void Enter()
        {
            _ended = false;
            _reloadClipLenght = _reloadClip.length;
            
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
            
            float speedMultiplier = _reloadClipLenght / GunStateMachine.Config.ReloadTime;
            _gunAnimator.SetFloat(ReloadSpeedMultiplier, speedMultiplier);//PARAMETRO EN LA ANIMACION DE RECARGA EN EL EDITOR
            
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