using System;
using _VanHelsingVR.Proyectile;
using RacTools.Variables;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using Range = _VanHelsingVR.Utilities.Range;

namespace _VanHelsingVR.Enemy
{
    public class FlyingEnemyStateMachine : EnemyStateMachine
    {
        [HideInInspector] public WayPointManager wayPointManager;
        
        [Title("States Params")]
        [SerializeField, Min(0.005f)] private float appearSpeed = 1;

        [SerializeField, Min(0.005f)] private float moveSpeed = 1;

        [SerializeField] private Range attackTimeRange;
        
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
        
        protected override void Start()
        {
            base.Start();
            AppearState = new AppearState(this, wayPointManager.GetNext(), appearSpeed);
            FlyAroundState = new FlyAroundState(this, attackTimeRange, moveSpeed, playerPosition.Value);
            FlyingAttackState = new FlyingAttackState(this);
            
            ChangeState(AppearState);
        }

        public void RestartAnimatorParams()
        {
            Animator.SetBool(DeadAnimationID, false);
            Animator.SetBool(AttackAnimationID, false);
            Animator.SetBool(FlyAnimationID, false);
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
        public void OnDead()
        {
            OnEnemyDead?.Invoke(this);
        }
    }
}
