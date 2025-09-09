using System.Collections;
using UnityEngine;

using _VanHelsingVR;
using _VanHelsingVR.Animation.Gun;
using _VanHelsingVR.Health;
using RacTools.Variables;

namespace _VR_Helsing.Gun
{
    public class ShootGunState : BaseGunState
    {
        private readonly Variable<int> _magazine;
        private readonly Transform _shootPoint;
        private readonly Transform _bulletParent;
        private readonly GameObject _bulletPrefab;
        private readonly Rigidbody _gunRigidbody;
        
        private readonly LayerMask _hittableLayer;
        private readonly GunInput _input;
        private GunConfig _config;

        private Coroutine _shootCor;
        private float _waitTime;
        
        private GunConfig Config => GunStateMachine.DataHandler.Config; //local Way To get config
        
        public ShootGunState(GunStateMachine stateMachine, GunStateFactory factory) : base(stateMachine, factory)
        {
            _magazine = GunStateMachine.DataHandler.Magazine;
            _input = GunStateMachine.Input;
            
            _bulletPrefab = GunStateMachine.BulletPrefab;
            _shootPoint = GunStateMachine.ShootPoint;
            _bulletParent = GunStateMachine.BulletParent;
            _gunRigidbody = GunStateMachine.GunRigidbody;
            _hittableLayer = GunStateMachine.HittableLayer;
        }

        public override void Enter()
        {
            _waitTime = 0.0f;
            _config = Config;
            _shootCor = GunStateMachine.StartCoroutine(Shoot());
        }

        public override void Exit()
        {
            if(_shootCor != null){
                GunStateMachine.StopCoroutine(_shootCor);
                _shootCor = null;
            }
        }

        public override void CheckState()
        {
            //Esto hace que aunque presiones el grip, si no sueltas el de disparo antes, no se cambia el estado
            if (!_input.IsTriggering)
            {
                _stateMachine.ChangeState(StateFactory.IdleState);
            }
        }

        IEnumerator Shoot()
        {
            HandleShot();
            
            if(_config.SingleShot) yield break;
            
            while(_input.IsTriggering)
            {
                _waitTime += Time.deltaTime;
                if (_waitTime > _config.ShootingSpeed)
                {
                    _waitTime = 0f;
                    HandleShot();
                }

                yield return null;
            };
        }

        private void HandleShot()
        {
            if (_magazine.Value > 0)
                NormalShot();
            else
                if (AudioManager.Instance)
                    AudioManager.Instance.PlaySound3D("DryShoot", GunStateMachine.transform.position);
        }

        private void NormalShot()
        {
            DoRecoil();

            var times = _config.UseMultiBirdShot ? _config.MultiBirdShotTimes : 1; 

            for (int i = 0; i < times; i++)
                CastShoot();
                
            if(AudioManager.Instance) AudioManager.Instance.PlaySound2D("Shoot_0"+Random.Range(1,8));
                
            _magazine.Value--;
            if (_magazine.Value <= 0)
                _magazine.Value = 0;
        }

        private void DoRecoil()
        {
            _gunRigidbody.AddForceAtPosition(-_shootPoint.forward * (_config.RecoilForce * 0.1f), _shootPoint.position);
            _gunRigidbody.AddForceAtPosition(_shootPoint.up * _config.RecoilForce, _shootPoint.position);
        }

        private void CastShoot()
        {
            RaycastHit hit;

            Vector3 direction = GetDirection();
            if (Physics.SphereCast(_shootPoint.position, Config.BulletRadius, direction, out hit, _config.FireRange, _hittableLayer))
            {
                var hurtbox = hit.transform.GetComponent<Hurtbox>();
                if(hurtbox != null)
                {
                    hurtbox.OnHitScan(GunStateMachine.FromLayer, _config.Damage);
                }
                else
                {
                    Debug.LogWarning("This Object does not have HurtBox Script", hit.transform.gameObject);
                }                
            }
            InstantiateVisualNormal(direction);
        }

        private Vector3 GetDirection()
        {
            Vector3 newDirection = GunStateMachine.transform.forward;
            newDirection += new Vector3(Random.Range(-_config.Spread, _config.Spread), UnityEngine.Random.Range(-_config.Spread, _config.Spread), UnityEngine.Random.Range(-_config.Spread, _config.Spread));
            newDirection.Normalize();
            return newDirection;
        }

        private void InstantiateVisualNormal(Vector3 direction)
        {
            Object.Instantiate(_bulletPrefab, _shootPoint.position, Quaternion.LookRotation(direction),_bulletParent);
        }

        
        //Despreciado maldito jeje
        private void InstantiateVisualTele(Vector3 direction, Vector3 target)
        {
            //Bullet actualBullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.LookRotation(direction),bulletParent).GetComponent<Bullet>();
            //muzzle.Play();
            //actualBullet.direction = (target - shootPoint.position).normalized;
        }
    }
}