using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;
using RacTools.Variables;
using _VanHelsingVR.Utilities;

namespace _VanHelsingVR.Conditions
{
    [Serializable]
    public class Condition
    {
        private enum ConditionType
        {
            Variable,
            Literal,
            TimeCondition
        }

        [SerializeField]
        private ConditionType conditionType;

        [SerializeField] private bool reversed;
    
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
    
#if UNITY_EDITOR
        [ShowIf(nameof(conditionType), ConditionType.TimeCondition)]
#endif
        [SerializeField]
        private TimeCondition timeCondition;

        public bool Value
        {
            get
            {
                bool value = conditionType switch
                {
                    ConditionType.Variable => variable.Value,
                    ConditionType.Literal => literal,
                    ConditionType.TimeCondition => timeCondition.IsTimerEnded,
                    _ => false
                };

                return reversed ? !value : value;
            }
        }

        public override string ToString()
        {
            var condition = this.conditionType switch
            {
                ConditionType.Variable => $"Variable '{variable.name}'",
                ConditionType.Literal => "Literal",
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