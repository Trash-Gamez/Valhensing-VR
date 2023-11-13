using System;
using System.Collections;
using System.Collections.Generic;
using _VanHelsingVR.Enemy;
using UnityEngine;

public class DesiredVelocityPos : MonoBehaviour
{
    [SerializeField] private CowardEnemyStateMachine stateMachine;

    private void Update()
    {
        if (stateMachine.RunawayState.DesiredVelocity.magnitude < 0.001f) return;
        
        transform.position = stateMachine.transform.position + stateMachine.RunawayState.DesiredVelocity;
    }
}
