using System.Collections;
using System.Linq;
using RacTools.RuntimeSet;
using RacTools.Variables;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _VanHelsingVR.Enemy
{
    [RequireComponent(typeof(Rigidbody))]
    public class EnemyGrounded : Enemy
    {
        
        [Title("IA config")] [Header("Behaviour Setting")] [SerializeField]
        private float maxForce;
        public RoomSpawner list;

        [Header("Seek")] [SerializeField] private VariableReference<Transform> target;
        [SerializeField] private float seekSpeed;

        [Header("Pursuit")] [SerializeField] private float pursuitSpeed;
        [SerializeField] private VariableReference<Vector3> targetVelocity;

        [Header("Avoid")] [SerializeField] float maxSeeAhead = 2.5f;
        [SerializeField] private float maxAvoidForce;
        [SerializeField] private float enemyRadius;
        [SerializeField] private RuntimeSet<Transform> enemies;

        [Header("Attack")] [SerializeField, Min(0.01f)]
        private float attackDistance;

        [Title("Animation")] [SerializeField] private Animator animator;
        [SerializeField] private AnimationClip attackAnimation;
        [SerializeField] private AnimationClip damagedAnimation;

        private static readonly int _IsAtackingProperty = Animator.StringToHash("IsAtacking");
        private static readonly int _IsWalking = Animator.StringToHash("IsWalking");
        
        private Coroutine _damagedCoroutine;
        private Coroutine _attackingCoroutine = null;
        private bool _isPursuiter;

        private Rigidbody _rb;
        private Vector3 ahead, ahead2;
        private Vector3 _desiredVelocity;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _isPursuiter = Random.value > 0.5f;
        }

        private void Update()
        {
            if (target == null || target.Value == null) return;
            
            if (_damagedCoroutine != null) return;
            if (_attackingCoroutine != null) return;
            RotateTowardsTarget();

            var targetDistance = Vector3.Distance(target.Value.position, transform.position);
            if(targetDistance <= attackDistance)
                Attack();
        }
        
        private void FixedUpdate()
        {
            Vector3 totalForce = Vector3.zero;

            totalForce = (_attackingCoroutine != null) ? Vector3.zero : CalculateTotalForce(totalForce);
            _rb.velocity = Vector3.ClampMagnitude(_rb.velocity + totalForce, maxForce);
            ClampYValue();
        }

        private void ClampYValue()
        {
            var vel = _rb.velocity;
            vel.y = 0;
            _rb.velocity = vel;
        }

        public override void OnDamage()
        {
            base.OnDamage();
            if(_attackingCoroutine != null)
                StopCoroutine(_attackingCoroutine);
            
            _damagedCoroutine = StartCoroutine(BeingDamageCor());
        }

        private IEnumerator BeingDamageCor()
        {
            _damagedCoroutine = null;
            yield break;
            animator.SetBool(_IsAtackingProperty, false);
            animator.SetBool(_IsWalking, false);

            yield return new WaitForSeconds(damagedAnimation.length);
                
        }
        
        private void Attack()
        {
            _rb.velocity = Vector3.zero;
            _attackingCoroutine = StartCoroutine(AttackCor());
        }
        
        private IEnumerator AttackCor()
        {
            animator.SetBool(_IsAtackingProperty, true);
            animator.SetBool(_IsWalking, false);
            
            yield return new WaitForSeconds(attackAnimation.length);
            
            animator.SetBool(_IsAtackingProperty, false);
            animator.SetBool(_IsWalking, true);
            
            _attackingCoroutine = null;
        }
        
        private void RotateTowardsTarget()
        {
            if (target == null || target.Value == null) return;
            
            var targetPos = target.Value.position;
            targetPos.y = transform.position.y;
            
            var lookDir = targetPos - transform.position;
            lookDir.Normalize();
            
            var rot = Quaternion.LookRotation(lookDir);
            transform.rotation = rot;
        } 
        
        #region Forces
        private Vector3 CalculateTotalForce(Vector3 totalForce)
        {
            if (target == null || target.Value == null) return Vector3.zero;
            
            totalForce += AvoidForce();
            totalForce += _isPursuiter ? PursuitForce() : SeekForce(target.Value.position);
            return totalForce;
        }

        private Vector3 PursuitForce()
        {
            if(targetVelocity == null || targetVelocity.Value == null) return Vector3.zero;
            
            var targetPos = target.Value.transform.position;
            var distance = Vector3.Distance(transform.position, targetPos);
            var t = (int)(distance / pursuitSpeed);

            var seekTarget = targetPos + targetVelocity.Value * t;
            return SeekForce(seekTarget);
        }

        private Vector3 SeekForce(Vector3 seekTarget)
        {
            var steering = Vector3.zero;
            _desiredVelocity = (seekTarget - transform.position);

            _desiredVelocity = _desiredVelocity.normalized * seekSpeed;

            steering = _desiredVelocity - _rb.velocity;

            return steering;
        }

        private Vector3 AvoidForce()
        {
            var velocity = _rb.velocity;
            var position = transform.position;

            ahead = position + velocity.normalized * maxSeeAhead;
            ahead2 = position + velocity.normalized * (maxSeeAhead * 0.5f);

            var mostThreating = FindBiggestThreat();
            var avoidance = Vector3.zero;

            if (mostThreating != null)
            {
                avoidance.x = ahead.x - mostThreating.Value.x;
                avoidance.y = ahead.y - mostThreating.Value.y;
                avoidance.Normalize();
                avoidance *= maxAvoidForce;
            }
            else
            {
                avoidance *= 0;
            }

            return avoidance;
        }

        private Vector3? FindBiggestThreat()
        {
            Vector3? mostThreating = null;

            var tempEnemies = enemies.Set
                .Where(enemy => enemy != transform)
                .ToList();
            
            for (int i = 0; i < tempEnemies.Count; i++)
            {
                var enemy = tempEnemies[i];
                var collision = CollisionDetected(ahead, ahead2, enemy.position);

                if (collision && (mostThreating == null || Vector3.Distance(transform.position, enemy.position) <
                        Vector3.Distance(transform.position, mostThreating.Value)))
                {
                    mostThreating = enemy.position;
                }
            }

            return mostThreating;
        }

        private bool CollisionDetected(Vector3 ahead, Vector3 ahead2, Vector3 obstacleCenter)
        {
            return Vector3.Distance(obstacleCenter, ahead) <= enemyRadius ||
                   Vector3.Distance(obstacleCenter, ahead2) <= enemyRadius;
        }

        #endregion
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, enemyRadius);
            
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, attackDistance);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            if (enemies != null)
                enemies.AddToSet(transform);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            if (enemies != null)
                enemies.RemoveFromSet(transform);
        }
        
        public void GetOff(){
            list.enemy.Remove(gameObject);
        }
        
    }
}
