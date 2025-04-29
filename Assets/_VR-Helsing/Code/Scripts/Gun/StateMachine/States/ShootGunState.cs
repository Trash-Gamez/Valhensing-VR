using System.Collections;
using UnityEngine;

using _VanHelsingVR;
using _VanHelsingVR.Health;
using RacTools.Variables;

namespace _VR_Helsing.Gun
{
    public class ShootGunState : BaseGunState
    {
        private Variable<int> _magazine;
        private Transform _shootPoint;
        private Transform _bulletParent;
        private GameObject _bulletPrefab;
        private Rigidbody _gunRigidbody;
        
        private LayerMask _hittableLayer;
        private GunConfig _config;
        
        public ShootGunState(GunStateMachine stateMachine, Variable<int> magazine) : base(stateMachine)
        {
            _magazine = magazine;
            
            _bulletPrefab = gunStateMachine.BulletPrefab;
            _shootPoint = gunStateMachine.ShootPoint;
            _bulletParent = gunStateMachine.BulletParent;
            _gunRigidbody = gunStateMachine.GunRigidbody;
            _hittableLayer = stateMachine.HittableLayer;
        }

        public override void Enter()
        {
            _config = gunStateMachine.Config;
            Shoot();
            gunStateMachine.ChangeState(gunStateMachine.IdleGunState);
        }
        
        public override void Exit()
        {
        }
        
        void Shoot()
        {
            if (_magazine.Value > 0)
            {
                _gunRigidbody.AddForceAtPosition(-_shootPoint.forward * (_config.RecoilForce * 0.1f), _shootPoint.position);
                _gunRigidbody.AddForceAtPosition(_shootPoint.up * _config.RecoilForce, _shootPoint.position);

                if (_config.UseMultiBirdShot)
                {
                    for (int i = 0; i < _config.MultiBirdShotTimes; i++)
                    {
                        var direction = MakeShoot();
                        InstantiateVisualNormal(direction);
                    }
                }
                else
                {
                    var direction = MakeShoot();
                    InstantiateVisualNormal(direction);
                }

                //if(AudioManager.Instance) AudioManager.Instance.PlaySound2D("Shoot_0"+Random.Range(1,8));
                
                _magazine.Value--;
                if (_magazine.Value <= 0)
                {
                    _magazine.Value = 0;
                }

                gunStateMachine.WaitShoot();
            }
            else
            {
                if(AudioManager.Instance) AudioManager.Instance.PlaySound3D("DryShoot", gunStateMachine.transform.position);
            
                gunStateMachine.WaitShoot();
            }
        }

        private Vector3 MakeShoot()
        {
            RaycastHit hit;

            Vector3 direction = GetDirection();
            if (Physics.SphereCast(_shootPoint.position, 0.25f, direction, out hit, _config.FireRange, _hittableLayer))
            {
                var hurtbox = hit.transform.GetComponent<Hurtbox>();
                if(hurtbox != null)
                {
                    hurtbox.OnHitScan(gunStateMachine.FromLayer, _config.Damage);
                }
                else
                {
                    Debug.LogWarning("This Object does not have HurtBox Script", hit.transform.gameObject);
                }                
            }

            return direction;
        }

        private Vector3 GetDirection()
        {
            Vector3 newDirection = gunStateMachine.transform.forward;
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