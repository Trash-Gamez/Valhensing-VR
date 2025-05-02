using System.Collections;
using UnityEngine;

using RacTools.Variables;
using RacTools.StateMachine;

namespace _VR_Helsing.Gun
{
    public class GunStateMachine : BaseStateMachine
    {
        public static readonly int IsLoading = Animator.StringToHash("IsLoading");
        
        private readonly int[] _magazinesLoad = new int[3];

        [Header("Gun Variables")]
        [SerializeField]
        private Variable<int> magazine;

        [Header("Gun Settings")]

        [SerializeField] private GunData[] guns;
        [SerializeField] private GunData data;
        [SerializeField] private GameObject fromGameObjectLayer;
        [SerializeField] private LayerMask hittableLayer;
        [SerializeField] private Renderer[] gunRenderers;

        [Header("Velocity")]
        [SerializeField] private float yVelocityThreshold;

        [Header("Gun Properties")]
        [SerializeField] private Rigidbody gunRigidbody;
        [SerializeField] private Animator gunAnimator;
        [SerializeField] private Transform shootPoint;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform bulletParent;
        [SerializeField] private GameObject bloodEffect;

        [Header("Gun Input")]
        [SerializeField] private GunInput gunInput;
        
        [Header("Animation")] 
        [SerializeField] private AnimationClip reloadClip;
        
        #region GETTERS & SETTERS
        
        //Magazines per gun
        public int[] MagazinesLoad => _magazinesLoad;
        
        //Variables
        public Variable<int> Magazine => magazine;

        #region SETTINGS
        //Settings
        public int CurrentGun
        {
            get => _currentGun;
            set
            {
                _currentGun = value;
            }
        }
        public float YVelocityThreshold => yVelocityThreshold;
        public GunData[] Guns => guns;
        public GunConfig Config => _config;
        public LayerMask HittableLayer => hittableLayer;
        public int FromLayer => fromGameObjectLayer.layer;
        #endregion
        
        #region GUN PROPERTIES
        //Gun Properties
        public Rigidbody GunRigidbody => gunRigidbody;
        public Animator GunAnimator => gunAnimator;
        public Transform ShootPoint => shootPoint;
        public GameObject BulletPrefab => bulletPrefab;
        public Transform BulletParent => bulletParent;
        #endregion
        
        //Input
        public GunInput Input => gunInput;
        //Animation
        public AnimationClip ReloadClip => reloadClip;

        //State Machine
        private GunStateFactory _stateFactory;
        public BaseState CurrentState => currentState;
        #endregion

        private GunConfig _config;
        private int _currentGun = 0;
        
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
