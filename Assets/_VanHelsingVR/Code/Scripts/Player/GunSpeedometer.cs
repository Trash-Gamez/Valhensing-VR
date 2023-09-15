using System;
using System.Collections;
using System.Collections.Generic;
using _VanHelsingVR.Variables;
using UnityEngine;

public class GunSpeedometer : MonoBehaviour
{
    [SerializeField] private Variable<float> xSpeedVar;

    private Transform _transform;
    private Vector3 _oldPosition = Vector3.zero;

    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        var deltaPosition = _transform.position.x - _oldPosition.x;
        xSpeedVar.Value = deltaPosition / Time.deltaTime;

        _oldPosition = _transform.position;
    }
}
