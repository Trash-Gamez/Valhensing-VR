using System;
using System.Collections;
using System.Collections.Generic;
using _VanHelsingVR.Variables;
using Sirenix.OdinInspector;
using UnityEngine;

public class TEST_HandVelocityTrack : MonoBehaviour
{
    [SerializeField]
    private FloatVariable _yPos;

    [SerializeField] 
    private FloatVariable _ySpeed;

    [SerializeField] 
    private FloatVariable _yDelta;

    [SerializeField] 
    private IntVariable _reloadedTimes;
    
    [SerializeField]
    private bool _useLocal = false;

    [SerializeField, Min(0.001f), BoxGroup("Realod Config")] private float _speedToReload;
    [SerializeField, BoxGroup("Realod Config")] private float _reloadCadence;
    private Coroutine _reloadCor = null;

    private void Start()
    {
        _reloadedTimes.Value = 0;
    }

    void Update()
    {
        var newPos = transform.localPosition.y;

        _yDelta.Value = newPos - _yPos.Value;

        _ySpeed.Value = _yDelta.Value / Time.deltaTime;
        
        _yPos.Value = newPos;

        if (Mathf.Abs(_ySpeed.Value) < _speedToReload) return;
        if (_reloadCor != null) return;
        
        _reloadCor = StartCoroutine(ReloadCor());    
    }

    IEnumerator ReloadCor()
    {
        _reloadedTimes.Value++;
        yield return new WaitForSeconds(_reloadCadence);
        _reloadCor = null;
    }
}
