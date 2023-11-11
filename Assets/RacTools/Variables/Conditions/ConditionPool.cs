using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _VanHelsingVR.Conditions
{
    [Serializable]
    /*
     * Esta clase se usa para hacer una concatenacion de condiciones, es decir una lista de condiciones
     * las cuales se deberan cumplir para poder realizar una accion, en pocas palabras es un bo
     */
    public class ConditionPool
    {
        [SerializeField] private List<Condition> conditions = new();

        public bool CanDo
        {
            get
            {
                if (conditions == null || conditions.Count <= 0)
                {
                    return true;
                }

                var canDo = true;
                
                //Solo si todas las condiciones se cumplen
                foreach (var condition in conditions)
                {
                    if(condition.Value) continue;
                    canDo = false;
                    break;
                }
                
                return canDo;
            }
        }

        public void LogVars()
        {
            foreach (var condition in conditions)
            {
                UnityEngine.Debug.Log(condition.ToString());
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
}
