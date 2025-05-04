using System.Collections;
using UnityEngine;

using RacTools.Variables;
using RacTools.StateMachine;

namespace _VR_Helsing.Gun
{
    public class GunStateMachine : BaseStateMachine
    {
        public static readonly int IsLoading = Animator.StringToHash("IsLoading");
        
        #region EVENTS
        //Alguna codigo de evento
        #endregion

        [Header("Gun Settings")] 
        [SerializeField] private GunDataHandler dataHandler;
        
        [SerializeField] private GameObject fromGameObjectLayer;
        [SerializeField] private LayerMask hittableLayer;

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
        
        #region SETTINGS
        //Settings
        public GunDataHandler DataHandler => dataHandler;
        public float YVelocityThreshold => yVelocityThreshold;
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

        
        private void Awake()
        {
            _stateFactory = new GunStateFactory(this);
            currentState = _stateFactory.IdleState;
            currentState.Enter();
        }
        
    }
}
