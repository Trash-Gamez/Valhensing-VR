using System;
using System.Collections;
using System.Collections.Generic;
using RacTools.Variables;
using UnityEngine;
using UnityEngine.Serialization;

[DefaultExecutionOrder(-1)]

public class SetTranformVariable : MonoBehaviour
{
    [SerializeField] private Transform transformToSet;
    [SerializeField] private Variable<Transform> variable;
    private void Start()
    {
        variable.Value = transformToSet;
    }
}
