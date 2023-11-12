using UnityEngine;
using _VanHelsingVR.IA;
using RacTools.Miscelaneous;
using RacTools.RuntimeSet;
using RacTools.Utilities;

namespace _VanHelsingVR.Enemy
{
    public class RunawayState : BaseEnemyState
    {
        private Quaternion _leftRot, _rightRot;
        
        private CowardEnemyStateMachine _cowardEnemyStateMachine;
        private Vector3 _velocity = Vector3.zero;
        private Vector3 _desiredVelocity = Vector3.zero;
        private float _maxForce;
        
        //RunAway Params
        private float _runAwayCircle;
        private float _safeRadius;
        private Transform _target;
        private float _speed;
        
        //Avoid Params
        private RuntimeSet<IAObstacle> _obstacles;
        private float _maxSeeAhead;
        private float _maxAvoidForce;
        
        //Avoid Vars
        private Vector3 _ahead, _ahead2, _leftAhead, _leftAhead2, _rightAhead, _rightAhead2;
        private Vector3 _position;
        
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
        }

        public override void OnStateEnter()
        {
            
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

            stateMachine.transform.position += _velocity * Time.deltaTime;
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
                var speedMultiplier = Map.MapFloatRange(distance, new Range(_safeRadius, _runAwayCircle), new Range(0,1), true);
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
            
            _leftAhead = Quaternion.AngleAxis(-15, stateMachine.transform.up) * _ahead;
            _leftAhead2 = Quaternion.AngleAxis(-15, stateMachine.transform.up) * _ahead;

            _rightAhead = Quaternion.AngleAxis(15, stateMachine.transform.up) * _ahead;
            _rightAhead2 = Quaternion.AngleAxis(15, stateMachine.transform.up) * _ahead2;
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