using System;
using System.Linq;
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

    public void LogVars()
    {
        foreach (var condition in conditions)
        {
            Debug.Log(condition.ToString());
        }
    }

    public void AddCondition(Condition condition)
    {
        if (conditions.Contains(condition)) return;
        conditions.Add(condition);
    }

    public void RemoveCondition(Condition condition)
    {
        if (!conditions.Contains(condition)) return;
        conditions.Remove(condition);
    }

    public static implicit operator bool(ConditionPool cp) => cp.CanDo;
}
