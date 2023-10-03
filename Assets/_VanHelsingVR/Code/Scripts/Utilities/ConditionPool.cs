using _VanHelsingVR.Variables;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ConditionPool
{
    [SerializeField] private List<Condition> conditions = new();

    public bool CanDo
    {
        get
        {
            if (!conditions.Any())
            {
                return true;
            }
            
            var canDo = conditions.All(variable => variable.Value);
            return canDo;
        }
    }

    public void AddCondition(Condition contition)
    {
        
    }

    public static implicit operator bool(ConditionPool cp) => cp.CanDo;
}
