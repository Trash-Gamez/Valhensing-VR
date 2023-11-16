using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using RacTools.Miscelaneous;
using RacTools.RuntimeSet;
using RacTools.Utilities;
using _VanHelsingVR.IA;

namespace _VanHelsingVR.Enemy
{
    public class RunawayState : BaseEnemyState
    {
        public Vector3 DesiredVelocity => _desiredVelocity;
        
        private Quaternion _leftRot, _rightRot;
        
        private readonly CowardEnemyStateMachine _cowardEnemyStateMachine;
        private Vector3 _velocity = Vector3.zero;
        private Vector3 _desiredVelocity = Vector3.zero;
        private readonly float _maxForce;
        
        //RunAway Params
        private readonly float _runAwayCircle;
        private readonly float _safeRadius;
        private readonly Transform _target;
        private readonly float _speed;
        
        //Avoid Params
        private readonly RuntimeSet<IAObstacle> _obstacles;
        private readonly float _maxSeeAhead;
        private readonly float _maxAvoidForce;
        
        //Avoid Vars
        private Vector3 _ahead, _ahead2, _leftAhead, _leftAhead2, _rightAhead, _rightAhead2;
        private Vector3 _position;
        
        private static readonly int _SpeedMultiplierAnim = Animator.StringToHash("SpeedMultiplier");
        private static readonly Range _SpeedMultiplierRange = new Range(1, 0.2f);
        private static Range _runAwayEnemyRadiusRange;

        public RunawayState(CowardEnemyStateMachine stateMachine, AvoidParams avoidParams, RunAwayParams runAwayParams, float maxForce) : base(stateMachine)
        {
            _cowardEnemyStateMachine = stateMachine;

            _runAwayCircle = runAwayParams.RunAwayCircle;
            _safeRadius = runAwayParams.SafeRadius;
            _target = runAwayParams.Target;
            _speed = runAwayParams.Speed;
            
            _obstacles = avoidParams.Obstacles;
            _maxSeeAhead = avoidParams.MaxSeeAhead;
            _maxAvoidForce = avoidParams.MaxAvoidForce;

            _maxForce = maxForce;

            _runAwayEnemyRadiusRange = new Range(_runAwayCircle, _safeRadius);
        }

        public override void OnStateEnter()
        {
            stateMachine.RestartAnimatorParams();
            stateMachine.Animator.SetLayerWeight(1, 0.75f);
            stateMachine.Animator.SetBool(CowardEnemyStateMachine.IsWalkingAnimID, true);
        }
        
        public override void OnStateUpdate()
        {
            _position = stateMachine.transform.position;
            var totalForce = Vector3.zero;
            
            var runAwayForce = RunAwayForce();
            var avoidForce = AvoidForce();

            totalForce = runAwayForce + avoidForce;
            
            _velocity = Vector3.ClampMagnitude(_velocity + totalForce, _maxForce);
            _velocity.y = 0;

            _cowardEnemyStateMachine.rb.velocity = _velocity;
            
            if (_velocity != Vector3.zero) 
                stateMachine.Animator.SetFloat(_SpeedMultiplierAnim,-_velocity.magnitude);
            else
                stateMachine.Animator.SetFloat(_SpeedMultiplierAnim, 0);
        }

        private Vector3 RunAwayForce()
        {
            _desiredVelocity = (_position - _target.position).normalized * _speed;
            
            var distance = (_target.position - _position).magnitude;
            
            if (distance > _safeRadius)
            {
                _desiredVelocity = Vector3.zero;
            }
            else if (distance > _runAwayCircle)
            {
                var speedMultiplier = Map.MapFloatRange(distance, _runAwayEnemyRadiusRange, _SpeedMultiplierRange, true);
                _desiredVelocity = _desiredVelocity.normalized * (_speed * speedMultiplier);
            }

            var steering = _desiredVelocity - _velocity;

            return steering;
        }
        
        #region Avoid Logic
        private Vector3 AvoidForce()
        {
            CalculateAhead();
            DrawAheads();
            
            var (mostThreating, ahead) = FindBiggestThreat();
            Vector3 avoidance = Vector3.zero;

            if (mostThreating != null)
            {
                avoidance.x = ahead.x - mostThreating.Value.x;
                avoidance.z = ahead.z - mostThreating.Value.z;
                avoidance.Normalize();
                avoidance *= _maxAvoidForce;
            }
            else
            {
                avoidance *= 0;
            }
            
            return avoidance;
        }

        private void CalculateAhead()
        {
            _ahead = _position + _velocity.normalized * _maxSeeAhead;
            _ahead2 = _position + _velocity.normalized * (_maxSeeAhead * 0.5f);
            
            _leftAhead = _position + (Quaternion.AngleAxis(-15, stateMachine.transform.up) * _ahead).normalized;
            _leftAhead2 = _position + (Quaternion.AngleAxis(-15, stateMachine.transform.up) * _ahead).normalized;

            _rightAhead = _position + (Quaternion.AngleAxis(15, stateMachine.transform.up) * _ahead).normalized;
            _rightAhead2 = _position + (Quaternion.AngleAxis(15, stateMachine.transform.up) * _ahead2).normalized;
        }

        private void DrawAheads()
        {
            DrawVectors(_ahead, Color.green);
            DrawVectors(_ahead2, Color.red);
            
            DrawVectors(_leftAhead, Color.green);
            DrawVectors(_leftAhead2, Color.red);
            
            DrawVectors(_rightAhead, Color.green);
            DrawVectors(_rightAhead2, Color.red);
        }
        
        private void DrawVectors(Vector3 vectorToDraw, Color color)
        {
            Debug.DrawLine(stateMachine.transform.position, vectorToDraw, color);
        }

        private (Vector3? mostThreating, Vector3 ahead) FindBiggestThreat()
        {
            Vector3? mostThreating = null;
            Vector3 ahead = Vector3.zero;

            foreach (var obstacle in _obstacles.Set)
            {
                var obstaclePos = obstacle.transform.position;
                
                var (collision, aheadCollision) = DetectAnyCollision(obstacle);

                if(collision && (mostThreating == null || Vector3.Distance(_position, obstacle.transform.position) < Vector3.Distance(_position, mostThreating.Value))){
                    mostThreating = obstaclePos;
                    ahead = aheadCollision;
                }
            }
        
            return (mostThreating, ahead);
        }

        private (bool, Vector3) DetectAnyCollision(IAObstacle obstacle)
        {
            var centerCollision = CollisionDetected(_ahead, _ahead2, obstacle);
            if (centerCollision) return (true, _ahead);
            
            var rightCollision = CollisionDetected(_rightAhead, _rightAhead2, obstacle);
            if (rightCollision) return (true, _rightAhead);
            
            var leftCollision = CollisionDetected(_leftAhead, _leftAhead2, obstacle);
            if (leftCollision) return (true, _leftAhead);
            
            return (false, Vector3.zero);
        }

        private bool CollisionDetected(Vector3 ahead, Vector3 ahead2, IAObstacle obstacle)
        {
            return Vector3.Distance(obstacle.Center, ahead) <= obstacle.Radius || Vector3.Distance(obstacle.Center, ahead2) <= obstacle.Radius;
        }
        #endregion
        
        public override void OnStateFixedUpdate()
        {
            return;
        }

        public override void OnStateExit()
        {
            
        }
    }
}