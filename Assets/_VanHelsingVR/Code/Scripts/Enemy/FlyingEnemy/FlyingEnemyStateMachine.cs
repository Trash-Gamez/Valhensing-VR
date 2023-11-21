using System;
using _VanHelsingVR.Proyectile;
using RacTools.Variables;
using Sirenix.OdinInspector;
using UnityEngine;
using Range = RacTools.Utilities.Range;

namespace _VanHelsingVR.Enemy
{
    public class FlyingEnemyStateMachine : EnemyStateMachine
    {
        [HideInInspector] public WayPointManager wayPointManager;
        
        [Title("States Params")]
        [SerializeField, Min(0.005f)] private float appearSpeed = 1;

        [SerializeField, Min(0.005f)] private float moveSpeed = 1;

        [SerializeField] private Range attackTimeRange;
        [SerializeField] private Health.Health health;
        
        [Title("Gun params")]
        [SerializeField] private SimpleProyectile bulletPrefab;
        [SerializeField] private Transform tipPos;
        public Transform bulletContainer;

        [Title("Proyectile Paramas")] 
        [SerializeField] private float proyectileSpeed;
        [SerializeField] private Variable<Transform> playerPosition;
        
        public static readonly int DeadAnimationID = Animator.StringToHash("IsDead");
        public static readonly int AttackAnimationID = Animator.StringToHash("IsAtacking");
        public static readonly int FlyAnimationID = Animator.StringToHash("IsFlying");

        public static event Action<FlyingEnemyStateMachine> OnEnemyDead;
        
        public AppearState AppearState { get; private set; }
        public FlyAroundState FlyAroundState {get; private set;}
        public FlyingAttackState FlyingAttackState { get; private set; }
        public FlyingDeadState FlyingDeadState { get; private set; }
        
        protected override void Start()
        {
            base.Start();
            AppearState = new AppearState(this, wayPointManager.GetNext(), appearSpeed);
            FlyAroundState = new FlyAroundState(this, attackTimeRange, moveSpeed, playerPosition.Value);
            FlyingAttackState = new FlyingAttackState(this);
            FlyingDeadState = new FlyingDeadState(this);
            
            ChangeState(AppearState);
        }

        public override void RestartAnimatorParams()
        { 
            Animator.SetBool(DeadAnimationID, false);
            Animator.SetBool(AttackAnimationID, false);
            Animator.SetBool(FlyAnimationID, false);
        }

        protected override void Update()
        {
            base.Update();
            if (health.CurrentHealth < 0 && !_isDead)
            {
                Debug.Log("Muerte sucia");
                OnDead();
            }
        }

        public void AttackEnd()
        {
            ChangeState(FlyAroundState);
        }

        public void Shoot()
        {
            //HAZ QUE DISPARE
            var simpleProyectile = Instantiate(bulletPrefab,tipPos.position, Quaternion.identity, bulletContainer);
            simpleProyectile.speed = proyectileSpeed;
            simpleProyectile.target = playerPosition.Value;
        }
        
        //Llamar desde el healthsystem de unity de este objeto
        private bool _isDead = false;
        public void OnDead()
        {
            if (_isDead) return;
            _isDead = true;
            
            OnEnemyDead?.Invoke(this);
            Debug.Log("Se murio, legal");
            ChangeState(FlyingDeadState);
        }
        
        //Cuando termina la animacion se destruye
        public void OnDeadAnimationEnd()
        {
            Destroy(gameObject);
        }
    }
}
