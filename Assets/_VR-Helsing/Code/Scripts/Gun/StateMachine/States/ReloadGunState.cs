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

        private GunConfig Config => GunStateMachine.DataHandler.Config; //local Way To get config
        private GunConfig _config;
        
        public ReloadGunState(GunStateMachine stateMachine, GunStateFactory factory) : base(stateMachine, factory)
        {
            _gunAnimator = GunStateMachine.GunAnimator;
            
            _reloadClip = GunStateMachine.ReloadClip;
            _magazine = GunStateMachine.DataHandler.Magazine;
        }

        public override void Enter()
        {
            _ended = false;
            _reloadClipLenght = _reloadClip.length;
            _config = Config;
            
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
            
            float speedMultiplier = _reloadClipLenght / _config.ReloadTime;
            _gunAnimator.SetFloat(ReloadSpeedMultiplier, speedMultiplier);//PARAMETRO EN LA ANIMACION DE RECARGA EN EL EDITOR
            
            if(AudioManager.Instance) AudioManager.Instance.PlaySound2D("Reload");
            
            yield return new WaitForSeconds(_config.ReloadTime);
            Reload();
            _ended = true;
        }
   
        private void Reload()
        {
            _magazine.Value += _config.ReloadBullets;
            if (_magazine.Value >= _config.MagazineSize)
            {
                if(AudioManager.Instance) AudioManager.Instance.PlaySound2D("FullReload");
                _magazine.Value = _config.MagazineSize;
            }
        }
    }
}