using System.Collections;
using System.Collections.Generic;
using _VanHelsingVR.Interaction;
using UnityEngine;

public class TEST_AttackState : TEST_State
{
    private Transform _target;
    private Transform _enemy;
    private Transform _boxOverlapPosition;
    private Vector3 _boxSize;
    private Collider[] _colliders = new Collider[1];
    private LayerMask _layerHit;
    
    private float _seekAgainRadius;
    private float _secondsForFirstHit;
    private float _secondsForEveryOtherHit;

    private Coroutine _hitCoroutine = null;
    
    public override void EnterState(TEST_EnemyStateMachine stateMachine)
    {
        _target = stateMachine.Target;
        _enemy = stateMachine.transform;
        _seekAgainRadius = stateMachine.SeekAgainRadius;
        _secondsForFirstHit = stateMachine.SecondsForFirstHit;
        _secondsForEveryOtherHit = stateMachine.SecondsForEveryOtherHit;
        _boxOverlapPosition = stateMachine.BoxOverlapPosition;
        _boxSize = stateMachine.BoxSize;
        _layerHit = stateMachine.LayerHit;
        Debug.Log("Entrando a pegar");
        _hitCoroutine = stateMachine.StartCoroutine(Hitcollider());
    }

    private IEnumerator Hitcollider()
    {
        yield return new WaitForSeconds(_secondsForFirstHit);
        Hit();
        while (true)
        {
            yield return new WaitForSeconds(_secondsForEveryOtherHit);
            Hit();
        }
    }

    private void Hit()
    {
        var hits = Physics.OverlapBoxNonAlloc(_boxOverlapPosition.position, _boxSize / 2, _colliders,
            Quaternion.identity, _layerHit);

        hits = Mathf.Clamp(hits, 0, _colliders.Length);

        for (int i = 0; i < hits; i++)
        {
            var collider = _colliders[i];
            Debug.Log("Pegue con: " + collider.name);
            if(!_colliders[i].TryGetComponent(out Damagable damagable)) continue;
            damagable.OnDamage();
        }
    }

    public override void UpdateState(TEST_EnemyStateMachine stateMachine)
    {
        
        var relativeTarget = _target.position;
        relativeTarget.y = _enemy.position.y;
        
        _enemy.LookAt(relativeTarget);
        
        if(Vector3.Distance(_enemy.position, relativeTarget) >= _seekAgainRadius)
            stateMachine.ChangeState(stateMachine.seekState);
    }

    public override void EndState(TEST_EnemyStateMachine stateMachine)
    {
        if (_hitCoroutine == null) return;
        stateMachine.StopCoroutine(_hitCoroutine);
        _hitCoroutine = null;
    }
}
