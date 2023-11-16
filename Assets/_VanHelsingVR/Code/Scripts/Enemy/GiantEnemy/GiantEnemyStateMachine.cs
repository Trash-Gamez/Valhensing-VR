using _VanHelsingVR.Proyectile;
using RacTools.Variables;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace _VanHelsingVR.Enemy
{
    public class GiantEnemyStateMachine : EnemyStateMachine
    {
        [Title("State Machine Params")]
        [field: SerializeField] public Rigidbody rb { get; private set; }
        
        [Title("Avoid Params")] 
        [SerializeField, Min(0.5f)] private float maxSeeAhead = 0.5f;
        [SerializeField, Min(0.2f)] private float maxAvoidForce = 0.2f;

        [Title("Follow Params")]
        [SerializeField] private VariableReference<Transform> target;
        [SerializeField, Min(0.1f)] private float FollowSpeed = 0.1f;

        [Title("Move Params")] 
        [SerializeField, Min(0.25f)] private float maxMoveForce = 0.25f;

        [Title("Proyectile")] 
        [SerializeField] private VariableReference<Transform> proyectileTip;
        [SerializeField] private ThrowingProyectile proyectilePrefab;
        [SerializeField, Min(0.25f)] private float proyectileSpeed = 2f;
        
        [Title("Attack")]
        [SerializeField, Min(0.25f)] private float attackRadius;
        [SerializeField] private RacTools.Utilities.Range timeToAttack;
        private float _attackCooldown = 0;
        private bool _isAttacking = false;
        
        public static readonly int IsAttackingAnimID = Animator.StringToHash("IsAtacking");
        public static readonly int IsWalkingAnimID = Animator.StringToHash("IsWalking");
        public static readonly int IsDeadAnimID = Animator.StringToHash("IsDead");
        
        public override void RestartAnimatorParams()
        {
            Animator.SetBool(IsAttackingAnimID, false);
            Animator.SetBool(IsWalkingAnimID, false);
            Animator.SetBool(IsDeadAnimID, false);
        }
    }
}