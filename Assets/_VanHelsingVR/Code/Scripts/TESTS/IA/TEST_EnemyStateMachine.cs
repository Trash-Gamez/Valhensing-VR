using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TEST_EnemyStateMachine : MonoBehaviour
{
    [Header("Seek Info")]
    public Transform Target;
    public float Speed;
    public float SeekRadius;
    
    [Header("Hit Info")]
    public Transform BoxOverlapPosition;
    public Vector3 BoxSize;
    public LayerMask LayerHit;
    public float SeekAgainRadius;
    public float SecondsForFirstHit;
    public float SecondsForEveryOtherHit;

    [Header("Backup Info")] 
    public float SeconsOfBackup;
    
    public TEST_State seekState = new TEST_SeekState();
    public TEST_State backUpState = new TEST_BackUpState();
    public TEST_State attackState = new TEST_AttackState();

    public TEST_State CurrentState = null;

    private void Start()
    {
        ChangeState(seekState);
    }

    private void Update()
    { 
        CurrentState.UpdateState(this);
    }
    
    public void ChangeState(TEST_State newState)
    {
        if(CurrentState != null)
            CurrentState.EndState(this);
        CurrentState = newState;
        CurrentState.EnterState(this);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, SeekRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(BoxOverlapPosition.position, BoxSize);
    }

    public void OnHit()
    {
        ChangeState(backUpState);
    }
}
