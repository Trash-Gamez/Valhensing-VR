using System;
using _VanHelsingVR.IA;
using _VanHelsingVR.Proyectile;
using RacTools.RuntimeSet;
using RacTools.Variables;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

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
        

        public static readonly int IsAttackingAnimID = Animator.StringToHash("IsAtacking");
        public static readonly int IsWalkingAnimID = Animator.StringToHash("IsWalking");
        public static readonly int IsDeadAnimID = Animator.StringToHash("IsDead");
        
        public RunawayState RunawayState { get; private set; }

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
                RunAwayCircle = runAwayCircle,
                SafeRadius = safeRadius,
                Target = target.Value,
                Speed = runAwaySpeed
            };

            RunawayState = new RunawayState(this, avoidParams, runAwayParams, maxMoveForce);
            
            ChangeState(RunawayState);
        }
        
        public override void RestartAnimatorParams()
        {
            Animator.SetLayerWeight(1, 0f);
            Animator.SetBool(IsAttackingAnimID, false);
            Animator.SetBool(IsWalkingAnimID, false);
            Animator.SetBool(IsDeadAnimID, false);
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
        }
        #endif
    }
}
