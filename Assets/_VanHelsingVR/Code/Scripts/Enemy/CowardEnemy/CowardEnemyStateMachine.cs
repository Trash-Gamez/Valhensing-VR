using System;
using _VanHelsingVR.IA;
using RacTools.RuntimeSet;
using RacTools.Variables;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _VanHelsingVR.Enemy
{
    public class CowardEnemyStateMachine : EnemyStateMachine
    {
        [SerializeField] private RuntimeSet<IAObstacle> obstacles;

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
            throw new NotImplementedException();
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
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(target.Value.position, runAwayCircle);
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(target.Value.position, safeRadius);
        }
#endif
    }
}
