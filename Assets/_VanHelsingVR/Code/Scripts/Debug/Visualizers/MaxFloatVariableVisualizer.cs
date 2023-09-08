

using System;
using UnityEngine;

public class MaxFloatVariableVisualizer : VariableVizualizer<float>
{
    [SerializeField, Min(0.1f)] private float _secondsToResetMax;

    private float _currentMaxFloat;

    protected override IObservable<float> GetObservable()
    {
        return base.GetObservable();
    }
}
