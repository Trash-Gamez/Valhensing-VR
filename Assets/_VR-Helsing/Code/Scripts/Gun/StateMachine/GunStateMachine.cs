using System;
using System.Collections;
using UnityEngine;
using TMPro;
using Autohand;
using Sirenix.OdinInspector;

using RacTools.Variables;
using RacTools.StateMachine;
using _VanHelsingVR;
using _VanHelsingVR.Animation.Gun;
using _VanHelsingVR.Health;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace _VR_Helsing.Gun
{
    public class GunStateMachine : BaseStateMachine
    {
        public static readonly int IsLoading = Animator.StringToHash("IsLoading");

        private readonly int[] _magazinesLoad = new int[3];
        public int[] MagazinesLoad => _magazinesLoad;
    
#if UNITY_EDITOR
        [Title("Gun Variables")]
#endif
        [SerializeField]
        private Variable<int> magazine;
    

#if UNITY_EDITOR
        [Title("Gun Settings")]
#endif
        [SerializeField] private GunData[] guns;
        [SerializeField] private GunData data;
        [SerializeField] private Renderer[] gunRenderers;
        [SerializeField] private float zAngularVelocityThreshold;
        [SerializeField] private float yVelocityThreshold;
        [SerializeField] private float xAngularVelocityThreshold;
        [SerializeField] private float secondsToChangeWeapon;
        [SerializeField] private GameObject fromGameObjectLayer;
        [SerializeField] private LayerMask hittableLayer;
        
        private GunConfig _config;
        private int _currentGun = 0;

        public int CurrentGun
        {
            get => _currentGun;
            set
            {
                _currentGun = value;
            }
        }
        public float ZAngularVelocityThreshold => zAngularVelocityThreshold;
        public float YVelocityThreshold => yVelocityThreshold;
        public float XAngularVelocityThreshold => xAngularVelocityThreshold;
        public GunData[] Guns => guns;
        public GunConfig Config => _config;
        public LayerMask HittableLayer => hittableLayer;
        public int FromLayer => fromGameObjectLayer.layer;

#if UNITY_EDITOR
        [Title("Gun Properties")]
#endif
        [SerializeField] private Rigidbody gunRigidbody;
        [SerializeField] private Animator gunAnimator;
        [SerializeField] private Transform shootPoint;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform bulletParent;
        [SerializeField] private GameObject bloodEffect;
 
        public Rigidbody GunRigidbody => gunRigidbody;
        public Animator GunAnimator => gunAnimator;
        public Transform ShootPoint => shootPoint;
        public GameObject BulletPrefab => bulletPrefab;
        public Transform BulletParent => bulletParent;
    
#if UNITY_EDITOR
        [Title("Gun Input")]
#endif
        [SerializeField] private GunInput gunInput;

        public GunInput Input => gunInput;
        
        private bool canReload =true;
        private bool canShoot = true;
        
        //STATES
        public IdleGunState IdleGunState { get; private set; }
        public ActiveGunState ActiveGunState { get; private set; }
        public ReloadGunState ReloadGunState { get; private set; }
        public ShootGunState ShootGunState { get; private set; }
        public ChangeWeaponGunState ChangeWeaponGunState { get; private set; }

        public bool CanReload = false; //TODO: hacer propiedad, medir cuando se dispara y esperar por aqui
        

        private void Awake()
        {
            IdleGunState = new IdleGunState(this, gunInput);
            ActiveGunState = new ActiveGunState(this, gunInput);
            ReloadGunState = new ReloadGunState(this, magazine);
            ShootGunState = new ShootGunState(this, magazine);
            ChangeWeaponGunState = new ChangeWeaponGunState(this, magazine);
        }

        private void Start()
        {
            magazine.Value = 0;
            _config = data.Config;
        }

        public void WaitShoot()
        {
            
        }

        private IEnumerator IWaitShootCor()
        {
            if (_config.SingleShot)
            {
                yield return new WaitUntil(() => !gunInput.IsTriggering);
            }
            else
            {
                yield return new WaitForSeconds(_config.ShootingSpeed);
            }
        }
        
        public void NextGun(int moveIndex)
        {
            MagazinesLoad[CurrentGun] = magazine.Value;
            CurrentGun += moveIndex;
            if (CurrentGun < 0) CurrentGun = Guns.Length - 1;
            if (CurrentGun >= Guns.Length) CurrentGun = 0;
        
            magazine.Value = MagazinesLoad[CurrentGun];
            ChangeGunData(Guns[CurrentGun]);
        }

        public void ChangeGunData(GunData newData)
        {
            if (newData.indexMesh < 0 || newData.indexMesh >= gunRenderers.Length) return;
        
            canShoot = true;
            canReload = true;
            //StopAllCoroutines();
            
            data = newData;
            _config = data.Config;

        
            DisableAllRenderers();
            gunRenderers[data.indexMesh].enabled = true;
        }

        private void DisableAllRenderers()
        {
            for (int i = 0; i < gunRenderers.Length; i++)
            {
                gunRenderers[i].enabled = false;
            }
        }
        
    }
}
