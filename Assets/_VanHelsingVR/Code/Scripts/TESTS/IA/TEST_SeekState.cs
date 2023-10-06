using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_SeekState : TEST_State
{
    private Transform _target;
    private Transform _enemy;
    private float _speed;
    private float _seekRadius;
    
    public override void EnterState(TEST_EnemyStateMachine stateMachine)
    {
        _target = stateMachine.Target;
        _enemy = stateMachine.transform;
        _speed = stateMachine.Speed;
        _seekRadius = stateMachine.SeekRadius;
    }

    public override void UpdateState(TEST_EnemyStateMachine stateMachine)
    {
        //_enemy.rotation = Quaternion.LookRotation(_target.position - _enemy.position, _enemy.up);
        
        var relativeTarget = _target.position;
        relativeTarget.y = _enemy.position.y;
        _enemy.LookAt(relativeTarget);
        
        _enemy.position = Vector3.MoveTowards(_enemy.position, relativeTarget, _speed * Time.deltaTime);
        
        if(Vector3.Distance(_enemy.position, relativeTarget) <= _seekRadius)
            stateMachine.ChangeState(stateMachine.attackState);
    }

    public override void EndState(TEST_EnemyStateMachine stateMachine)
    {
        
    }
}
