using System.Collections;
using UnityEngine;
using Sirenix.OdinInspector;

using RacTools.Variables;
using RacTools.StateMachine;
using _VanHelsingVR.Animation.Gun;
using _VR_Helsing.Utils;

namespace _VR_Helsing.Gun
{
    public class GunStateMachine : BaseStateMachine
    {
        public static readonly int IsLoading = Animator.StringToHash("IsLoading");

        public static readonly GunBuffer ZLocalAngVelBuffer = new GunBuffer();
        public static readonly GunBuffer XLocalAngVelBuffer = new GunBuffer();
        public static readonly GunBuffer YLocalVelBuffer = new GunBuffer();
        
        
        private readonly int[] _magazinesLoad = new int[3];
        public int[] MagazinesLoad => _magazinesLoad;
    
#if UNITY_EDITOR
        [Title("Gun Variables")]
#endif
        [SerializeField]
        private Variable<int> magazine;
    
        public Variable<int> Magazine => magazine;
#if UNITY_EDITOR
        [Title("Gun Settings")]
#endif
        [SerializeField] private GunData[] guns;
        [SerializeField] private GunData data;
        [SerializeField] private GameObject fromGameObjectLayer;
        [SerializeField] private LayerMask hittableLayer;
        [SerializeField] private Renderer[] gunRenderers;
#if UNITY_EDITOR
        [Title("Velocity")]
#endif
        [SerializeField] private float zAngularVelocityThreshold;
        [SerializeField] private float yVelocityThreshold;
        [SerializeField] private float xAngularVelocityThreshold;
        [SerializeField] private float secondsToChangeWeapon;
        private Vector3 _localAngularVelocity;

        public float MaxSecondsToChangeWeapon => secondsToChangeWeapon;
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
        public Vector3 LocalAngularVelocity => _localAngularVelocity;
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

        private GunStateFactory _stateFactory;
        public BaseState CurrentState => currentState; 

        public bool CanReload = false; //TODO: hacer propiedad, medir cuando se dispara y esperar por aqui

        private void Awake()
        {
            _stateFactory = new GunStateFactory(this);
            currentState = _stateFactory.IdleState;
            currentState.Enter();
        }

        private void Start()
        {
            magazine.Value = 0;
            _config = data.Config;
        }

        protected override void FixedUpdate()
        {
            _localAngularVelocity = gunRigidbody.GetLocalAngularVelocity();
            Vector3 localVelocity = gunRigidbody.transform.InverseTransformDirection(gunRigidbody.linearVelocity);
            
            XLocalAngVelBuffer.Add(_localAngularVelocity.x);
            ZLocalAngVelBuffer.Add(_localAngularVelocity.z);
            YLocalVelBuffer.Add(localVelocity.y);
            
            base.FixedUpdate();
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
