using _VanHelsingVR.Variables;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ConditionPool
{
    [SerializeField] private List<Condition> conditions;

    public bool CanDo
    {
        get
        {
            var canDo = conditions.All(variable =>
            {
                return variable.Value;
            });
            return canDo;
        }
    }

    public static implicit operator bool(ConditionPool cp) => cp.CanDo;
}
