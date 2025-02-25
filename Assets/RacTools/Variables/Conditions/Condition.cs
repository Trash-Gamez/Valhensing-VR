using System;
using Sirenix.OdinInspector;
using UnityEngine;
using RacTools.Variables;

namespace _VanHelsingVR.Conditions
{
    [Serializable]
    public class Condition
    {
        private enum ConditionType
        {
            Variable,
            Constant,
        }

        [SerializeField]
        private ConditionType conditionType;

        [HideIf(nameof(conditionType), ConditionType.Constant)]
        [SerializeField] private bool reversed;
        
        [ShowIf(nameof(conditionType), ConditionType.Constant)]
        [SerializeField]
        private bool constant;

        [ShowIf(nameof(conditionType), ConditionType.Variable)]
        [SerializeField]
        private Variable<bool> variable;

        public bool Value
        {
            get
            {
                bool value = conditionType switch
                {
                    ConditionType.Variable => variable.Value,
                    ConditionType.Constant => constant,
                    _ => false
                };

                //Si la condición es constante, no se hace el reversed
                if (conditionType == ConditionType.Constant) return value;
                
                return reversed ? !value : value;
            }
        }

        public override string ToString()
        {
            var condition = this.conditionType switch
            {
                ConditionType.Variable => $"Variable '{variable.name}'",
                ConditionType.Constant => "Literal",
                _ => throw new ArgumentOutOfRangeException()
            };
            var value = reversed ? !Value : Value;
            return $"{condition}: {value}";
        }
    }
}