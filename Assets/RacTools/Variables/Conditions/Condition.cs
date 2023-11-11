using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;
using RacTools.Variables;
using _VanHelsingVR.Utilities;
using UnityEngine.Serialization;

namespace _VanHelsingVR.Conditions
{
    [Serializable]
    public class Condition
    {
        private enum ConditionType
        {
            Variable,
            Constant,
            TimeCondition
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
        
        [ShowIf(nameof(conditionType), ConditionType.TimeCondition)]
        [SerializeField]
        private TimeCondition timeCondition;

        public bool Value
        {
            get
            {
                bool value = conditionType switch
                {
                    ConditionType.Variable => variable.Value,
                    ConditionType.Constant => constant,
                    ConditionType.TimeCondition => timeCondition.IsTimerEnded,
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
                ConditionType.TimeCondition => "Time Condition",
                _ => throw new ArgumentOutOfRangeException()
            };
            var value = reversed ? !Value : Value;
            return $"{condition}: {value}";
        }
    }

    #region CONDITIONS
    [Serializable]
    public sealed class TimeCondition : IInitializable, IDisposable
    {
        [Inject] private Timer timer;

        private bool _isTimerEnded = false;
        public bool IsTimerEnded => _isTimerEnded;

        private void OnTimerEnds()
        {
            _isTimerEnded = true;
        }
    
        private void OnTimerStarted()
        {
            _isTimerEnded = false;
        }

        public void Initialize()
        {
            UnityEngine.Debug.Log("Inicializando Timer");
            timer.OnTimerInitialized += OnTimerStarted;
            timer.OnTimerEnded += OnTimerEnds;
        }

        public void Dispose()
        {
            timer.OnTimerInitialized -= OnTimerStarted;
            timer.OnTimerEnded -= OnTimerEnds;
        }
    } 
    #endregion
}