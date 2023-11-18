using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using Sirenix.OdinInspector;
using _VanHelsingVR.IA;
using RacTools.Variables;
using RacTools.RuntimeSet;

namespace _VanHelsingVR.Enemy
{
    public class GiantEnemyStateMachine : EnemyStateMachine
    {
        [Title("State Machine Params")]
        [SerializeField] private RuntimeSet<IAObstacle> obstacles;
        [field: SerializeField] public Rigidbody rb { get; private set; }
        
        [Title("Avoid Params")] 
        [SerializeField, Min(0.5f)] private float maxSeeAhead = 0.5f;
        [SerializeField, Min(0.2f)] private float maxAvoidForce = 0.2f;

        [Title("Follow Params")]
        [SerializeField] private VariableReference<Transform> target;
        [SerializeField, Min(0.1f)] private float FollowSpeed = 0.1f;

        [Title("Move Params")] 
        [SerializeField, Min(0.25f)] private float maxMoveForce = 0.25f;
        [SerializeField, Min(0.25f)] private float detectPlayerRadius = 0.25f;
        [field: SerializeField, Range(1f,3f)] public float speedMultiplier { get; private set; } = 1;
        
        [Title("Attack")]
        [field: SerializeField, Min(0.25f)] public float attackRadius { get; private set; }
        [SerializeField] private Explosion.Explosion explosionAttack;
        
        private float _attackCooldown = 0;
        private bool _isAttacking = false;
        
        public static readonly int IsAttackingAnimID = Animator.StringToHash("IsAttacking");
        public static readonly int IsWalkingAnimID = Animator.StringToHash("IsWalking");
        public static readonly int IsDeadAnimID = Animator.StringToHash("IsDead");
        
        public GiantIdleState GiantIdleState { get; private set; }
        public FollowingState FollowingState { get; private set; }
        public GiantAttackingEnemyState GiantAttackingEnemyState { get; private set; }
        public GiantDeadState GiantDeadState { get; private set; }

        protected override void Start()
        {
            base.Start();

            var avoidParams = new AvoidParams()
            {
                Obstacles = obstacles,
                MaxAvoidForce = maxAvoidForce,
                MaxSeeAhead = maxSeeAhead
            };

            var runAwayParams = new RunAwayParams()
            {
                Target = target.Value,
                Speed = FollowSpeed
            };
            
            GiantIdleState = new GiantIdleState(this, target.Value, detectPlayerRadius);
            FollowingState = new FollowingState(this, avoidParams, runAwayParams, maxMoveForce);
            GiantAttackingEnemyState = new GiantAttackingEnemyState(this);
            GiantDeadState = new GiantDeadState(this);
            
            ChangeState(GiantIdleState);
        }

        public void OnDamage()
        {
            ChangeState(FollowingState);
        }

        public void OnAttack()
        {
            explosionAttack.DoExplosion(1.5f);
        }

        public void OnAttackEnd()
        {
            StartCoroutine(ReturnToFollowing());
        }

        private IEnumerator ReturnToFollowing()
        {
            yield return new WaitForSeconds(0.5f);
            ChangeState(FollowingState);    
        }

        public void OnDead()
        {
            ChangeState(GiantDeadState);
        }
        
        public void OnDeadAnimationEnd()
        {
            Destroy(gameObject);
        }

        public override void RestartAnimatorParams()
        {
            Animator.SetBool(IsAttackingAnimID, false);
            Animator.SetBool(IsWalkingAnimID, false);
            Animator.SetBool(IsDeadAnimID, false);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(transform.position, detectPlayerRadius);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRadius);
        }
    }
}