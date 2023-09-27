using _VanHelsingVR.Variables;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
using Zenject;

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
            return conditionType switch
            {
                ConditionType.Variable => variable.Value,
                ConditionType.Literal => literal,
                ConditionType.TimeCondition => timeCondition.IsTimerEnded,
                _ => false
            };
        }
    }
}


#region CONDITIONS
[Serializable]
public sealed class TimeCondition : IInitializable, IDisposable
{
    [SerializeField] private Timer timer;

    private bool _isTimerEnded = true;
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
        timer.OnTimerEnded += OnTimerEnds;
        timer.OnTimerInitialized += OnTimerStarted;
    }

    public void Dispose()
    {
        timer.OnTimerEnded -= OnTimerEnds;
        timer.OnTimerInitialized -= OnTimerStarted;
    }
} 
#endregion
