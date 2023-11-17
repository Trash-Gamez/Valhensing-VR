using System;
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
        
        [Title("Attack")]
        [SerializeField, Min(0.25f)] private float attackRadius;
        
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
            
            ChangeState(GiantIdleState);
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