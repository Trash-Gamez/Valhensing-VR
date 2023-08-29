using System;
using System.Collections;
using System.Collections.Generic;
using _VanHelsingVR.Variables;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Sirenix.OdinInspector;

public abstract class VariableVizualizer<T> : MonoBehaviour
{
    [SerializeField] private Variable<T> _variable;

    [SerializeField] private Text _text;

    [SerializeField] private bool _useParamString;
    [SerializeField, ShowIf(nameof(_useParamString))] private string _paramString;
    private void Start()
    {
        if (_useParamString)
        {
            _variable.OnValueChanged.SubscribeWithState(_text, (f, text) =>
                {
                    var newString = String.Format(_paramString, f.ToString());
                    _text.text = newString;
                })
                .AddTo(this);
        }
        else
        {
            _variable.OnValueChanged.SubscribeToText(_text)
                .AddTo(this);
        }
    }
}
