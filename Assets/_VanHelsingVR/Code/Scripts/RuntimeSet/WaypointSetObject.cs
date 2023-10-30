using System;
using System.Collections;
using System.Collections.Generic;
using RacTools.RuntimeSet;
using UnityEngine;

public class WaypointSetObject : MonoBehaviour
{
    [SerializeField] private RuntimeSet<Transform> wayPoints;
    
    private void OnEnable()
    {
        wayPoints.AddToSet(transform);
    }

    private void OnDisable()
    {
        wayPoints.RemoveFromSet(transform);
    }
}
