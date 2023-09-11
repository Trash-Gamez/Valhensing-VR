using System;
using System.Collections;
using System.Collections.Generic;
using _VanHelsingVR.Variables;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Sirenix.OdinInspector;
using UnityEngine.Serialization;
using TMPro;

public abstract class VariableVizualizer<T> : MonoBehaviour
{
    [SerializeField] private Variable<T> _variable;

    [SerializeField] private Text _text;
    private Text

    [SerializeField] private bool _useParamString;
    [SerializeField, ShowIf(nameof(_useParamString))] private string _paramString;

    [SerializeField, HideInPlayMode] private bool _useSample;
    [FormerlySerializedAs("_throttleSeconds")] [SerializeField, HideInPlayMode, ShowIf(nameof(_useSample)) ] private float _sampleSeconds;
    
    protected virtual void Start()
    {
        var onValueChanged = _variable.OnValueChanged;
        if (_useParamString)
        {
            if (_useSample)
                onValueChanged = onValueChanged.Sample(TimeSpan.FromSeconds(_sampleSeconds));
            
            onValueChanged.SubscribeToText(_text, (f) =>
                {
                    var newString = string.Format(_paramString, f.ToString());
                    return newString;
                })
                .AddTo(this);

            return;
        }

        if (_useSample)
            onValueChanged = onValueChanged.Sample(TimeSpan.FromSeconds(_sampleSeconds));

        onValueChanged.SubscribeToText(_text)
            .AddTo(this);
    }

    protected virtual IObservable<T> GetObservable()
    {
        return _variable.OnValueChanged;
    }
}
