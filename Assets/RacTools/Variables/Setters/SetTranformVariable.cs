using System;
using System.Collections;
using System.Collections.Generic;
using RacTools.Variables;
using UnityEngine;
using UnityEngine.Serialization;

public class SetTranformVariable : MonoBehaviour
{
    [FormerlySerializedAs("transform")] [SerializeField] private Transform t;
    [SerializeField] private Variable<Transform> variable;
    private void Start()
    {
        variable.Value = t;
    }
}
