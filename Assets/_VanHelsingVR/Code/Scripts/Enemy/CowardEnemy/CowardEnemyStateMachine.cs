using System;
using System.Collections;
using _VanHelsingVR.IA;
using _VanHelsingVR.Proyectile;
using RacTools.RuntimeSet;
using RacTools.Variables;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _VanHelsingVR.Enemy
{
    public class CowardEnemyStateMachine : EnemyStateMachine
    {
        [SerializeField] private RuntimeSet<IAObstacle> obstacles;
        [field: SerializeField] public Rigidbody rb { get; private set; }

        [Title("Avoid Params")] 
        [SerializeField, Min(0.5f)] private float maxSeeAhead = 0.5f;
        [SerializeField, Min(0.2f)] private float maxAvoidForce = 0.2f;

        [Title("Run Away Params")] 
        [SerializeField, Min(0.001f)] private float runAwayCircle = 0.001f;
        [SerializeField, Min(0.1f)] private float safeRadius = 0.1f;
        [SerializeField] private VariableReference<Transform> target;
        [SerializeField, Min(0.1f)] private float runAwaySpeed = 0.1f;

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

        private Transform _transform;
        private bool _firstAttack = false;
        private float _attackingTime;

        public static readonly int IsAttackingAnimID = Animator.StringToHash("IsAtacking");
        public static readonly int IsWalkingAnimID = Animator.StringToHash("IsWalking");
        public static readonly int IsDeadAnimID = Animator.StringToHash("IsDead");
        
        public RunawayState RunawayState { get; private set; }
        public DeadState DeadState { get; private set; }

        private void Awake()
        {
            _transform = transform;
        }

        protected override void Start()
        {
            base.Start();

            _attackingTime = UnityEngine.Random.Range(timeToAttack.Min, timeToAttack.Max);

            var avoidParams = new AvoidParams()
            {
                Obstacles = obstacles,
                MaxAvoidForce = maxAvoidForce,
                MaxSeeAhead = maxSeeAhead
            };

            var runAwayParams = new RunAwayParams()
            {
                RunAwayCircle = runAwayCircle,
                SafeRadius = safeRadius,
                Target = target.Value,
                Speed = runAwaySpeed
            };

            RunawayState = new RunawayState(this, avoidParams, runAwayParams, maxMoveForce);
            DeadState = new DeadState(this);
            
            ChangeState(RunawayState);
        }
        
        public override void RestartAnimatorParams()
        {
            Animator.SetLayerWeight(1, 0f);
            Animator.SetBool(IsAttackingAnimID, false);
            Animator.SetBool(IsWalkingAnimID, false);
            Animator.SetBool(IsDeadAnimID, false);
        }

        protected override void Update()
        {
            base.Update();
            
            if (_isAttacking) return;
            if ((target.Value.position - _transform.position).magnitude > attackRadius)
            {
                _attackCooldown = 0;
                return;
            }

            if (!_firstAttack)
            {
                Attack();
                _firstAttack = true;
                return;
            }
            
            _attackCooldown += Time.deltaTime;
            if (_attackCooldown >= _attackingTime)
            {
                Attack();
            }
        }

        private void Attack()
        {
            _isAttacking = true;
            StartCoroutine(AttackCor());
        }
        
        private IEnumerator AttackCor()
        {
            Animator.SetBool(IsWalkingAnimID, false);
            Animator.SetBool(IsAttackingAnimID, true);

            yield return new WaitForSeconds(Animator.GetAnimatorTransitionInfo(0).duration);
            yield return new WaitForSeconds(Animator.GetCurrentAnimatorClipInfo(0)[0].clip.length);
            
            Animator.SetBool(IsWalkingAnimID, true);
            Animator.SetBool(IsAttackingAnimID, false);

            _isAttacking = false;
            _attackCooldown = 0;
        }

        private ThrowingProyectile _currentProyectile = null; 
        public void AppearProyectile()
        {
            _currentProyectile = Instantiate(proyectilePrefab, proyectileTip.Value.position, Quaternion.identity);
            _currentProyectile.Init(target.Value, proyectileSpeed);
        }

        public void ThrowProyectile()
        {
            if (_currentProyectile == null) return;
            _currentProyectile.Throw();
            _currentProyectile = null;
        }
        
        //Llamar desde el healthsystem de unity de este objeto
        public void OnDead()
        {
            ChangeState(DeadState);
        }
        
        //Cuando termina la animacion se destruye
        public void OnDeadAnimationEnd()
        {
            Destroy(gameObject);
        }
        
        #if UNITY_EDITOR
        private void OnValidate()
        {
            if (runAwayCircle > safeRadius)
            {
                safeRadius = runAwayCircle;
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (target.Value == null) return;
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(target.Value.position, runAwayCircle);
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(target.Value.position, safeRadius);
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(transform.position, attackRadius);
        }
        #endif
    }
}
