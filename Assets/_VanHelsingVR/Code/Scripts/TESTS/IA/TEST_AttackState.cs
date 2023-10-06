using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_AttackState : TEST_State
{
    private Transform _boxOverlapPosition;
    private Vector3 _boxSize;
    private Collider[] _colliders = new Collider[1];
    private LayerMask _layerHit;
    public override void EnterState(TEST_EnemyStateMachine stateMachine)
    {
        _boxOverlapPosition = stateMachine.BoxOverlapPosition;
        _boxSize = stateMachine.BoxSize;
        _layerHit = stateMachine.LayerHit;
        Debug.Log("Entrando a pegar");
        stateMachine.StartCoroutine(Hitcollider());
    }

    private IEnumerator Hitcollider()
    {
        yield return new WaitForSeconds(0.3f);
        var hits = Physics.OverlapBoxNonAlloc(_boxOverlapPosition.position, _boxSize / 2, _colliders,
            Quaternion.identity, _layerHit);

        hits = Mathf.Clamp(hits, 0, _colliders.Length);

        for (int i = 0; i < hits; i++)
        {
            var collider = _colliders[i];
            Debug.Log("Pegue con: " + collider.name);
            //if(_colliders[i].TryGetComponent())
        }
    }

    public override void UpdateState(TEST_EnemyStateMachine stateMachine)
    {
        return;
    }

    public override void EndState(TEST_EnemyStateMachine stateMachine)
    {
        throw new System.NotImplementedException();
    }
}
