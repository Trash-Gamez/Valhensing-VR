using System;
using System.Collections;
using RacTools.Variables;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;
using Range = _VanHelsingVR.Utilities.Range;

namespace _VanHelsingVR.Enemy
{
    [RequireComponent(typeof(Rigidbody))]
    public class FlyingEnemy : Enemy
    {
        public static event Action<Transform> OnFlyingEnemyDead;
        
        public Transform currentWaypoint = null;
        
        [Title("IA config")]
        [Header("Behaviour Setting")]
        [SerializeField] private float maxForce;
        
        [Header("Seek")] 
        [SerializeField] private float seekSpeed;
        [SerializeField] private float slowingRadius;

        [Header("Proyectile")] [SerializeField]
        private HittableProyectile proyectilePrefab;
        [SerializeField] private VariableReference<Transform> proyectileTip;
        [SerializeField] private VariableReference<Transform> targetProyectile;
        [SerializeField] private Range secondsRangeRandomizer;
        
        [Title("Animation")] [SerializeField] private Animator animator;
        [SerializeField] private AnimationClip attackAnimation;
        
        private Rigidbody _rb;
        private Vector3 _desiredVelocity;
        private static readonly int _IsAtacking = Animator.StringToHash("IsAtacking");
        private static readonly int _IsFlying = Animator.StringToHash("IsFlying");

        protected override void Awake()
        {
            base.Awake();
            _rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            StartCoroutine(RandomShootCor());
        }

        private void Update()
        {
            RotateTowardsTarget();
        }

        private void FixedUpdate()
        {
            Vector3 totalForce = Vector3.zero;
            totalForce += SeekForce(currentWaypoint.position);

            _rb.velocity = Vector3.ClampMagnitude(_rb.velocity + totalForce, maxForce);
            ClampYValue();
        }

        private IEnumerator RandomShootCor()
        {
            yield return new WaitForSeconds(Random.Range(secondsRangeRandomizer.Min, secondsRangeRandomizer.Max));
            
            animator.SetBool(_IsAtacking, true);
            animator.SetBool(_IsFlying, false);
            
            yield return new WaitForSeconds(attackAnimation.length);
            
            animator.SetBool(_IsAtacking, false);
            animator.SetBool(_IsFlying, true);
            
            StartCoroutine(RandomShootCor());
        }
        
        private void RotateTowardsTarget()
        {
            if (targetProyectile == null || targetProyectile.Value == null) return;
            var dir = targetProyectile.Value.position - transform.position;
            dir.Normalize();
            var rot = Quaternion.LookRotation(dir);
            transform.rotation = rot;
        }
        
        //Esta funcion se manda a llamar desde el animator del enemigo volador, y es un evento que se manda allamar en la animacion 
        // de atacar
        public void LaunchProyectile()
        {
            if (targetProyectile == null || targetProyectile.Value == null) return;
            var positionSpawn = proyectileTip != null ? proyectileTip.Value.position : transform.position;
            var proyectile = Instantiate(proyectilePrefab,positionSpawn, Quaternion.identity);
            proyectile.target = targetProyectile.Value;
        }
        
        //Que no se mueva en y la velocidad del enemigo volador
        private void ClampYValue()
        {
            var vel = _rb.velocity;
            vel.y = 0;
            _rb.velocity = vel;
        }
        
        private Vector3 SeekForce(Vector3 seekTarget)
        {
            var steering = Vector3.zero;
            _desiredVelocity = (seekTarget - transform.position);

            float distance = _desiredVelocity.magnitude;

            _desiredVelocity = _desiredVelocity.normalized * seekSpeed * (distance / slowingRadius);;

            steering = _desiredVelocity - _rb.velocity;

            return steering;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            OnFlyingEnemyDead?.Invoke(currentWaypoint);
        }
    }
}
