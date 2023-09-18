using System;
using UnityEngine;
using _VanHelsingVR.Variables;

#if UNITY_EDITOR
using Sirenix.OdinInspector;
#endif

public class SpeedoMeter : MonoBehaviour
{
    private enum VarType
    {
        X ,
        Y,
        Z,
        Vector
    }

    [SerializeField] private VarType varType = VarType.X;

    #if UNITY_EDITOR
    [HideIf(nameof(varType), VarType.Vector)]
    #endif
    [SerializeField]
    private Variable<float> speedVar;
    

    #if UNITY_EDITOR
    [ShowIf(nameof(varType), VarType.Vector)]
    #endif 
    [SerializeField]
    private Variable<Vector3> velocityVar;
    

    private Transform _transform;
    private Vector3 _oldPosition = Vector3.zero;

    private Vector3 _velocity;

    private void Awake() => _transform = transform;

    private void Update()
    {
        var currentPos = _transform.position;
        var deltaPosition = currentPos - _oldPosition;
        _velocity = deltaPosition / Time.deltaTime;
        SetSpeedVar();

        _oldPosition = currentPos;
    }

    private void SetSpeedVar()
    {
        switch (varType)
        {
            case VarType.Vector:
                if(velocityVar != null)
                    velocityVar.Value = _velocity;
                break;
            case VarType.X:
                if(speedVar != null)
                    speedVar.Value = _velocity.x;
                break;
            case VarType.Y:
                if(speedVar != null)
                    speedVar.Value = _velocity.y;
                break;
            case VarType.Z:
                if(speedVar != null)
                    speedVar.Value = _velocity.z;
                break;
            default:
                break;
        }
    }
}
