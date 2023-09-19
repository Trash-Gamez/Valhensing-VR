using _VanHelsingVR.Variables;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Condition
{
    private enum ConditionType
    {
        Variable,
        Literal
    }

    [SerializeField]
    private ConditionType conditionType;
    
    #if UNITY_EDITOR
    [ShowIf(nameof(conditionType), ConditionType.Literal)]
    #endif
    [SerializeField]
    private bool literal;

    #if UNITY_EDITOR
    [ShowIf(nameof(conditionType), ConditionType.Variable)]
    #endif
    [SerializeField]
    private Variable<bool> variable;

    public bool Value
    {
        get
        {
            return conditionType switch
            {
                ConditionType.Variable => variable.Value,
                ConditionType.Literal => literal,
                _ => false
            } ;
        }
    }
}
