using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using UniRx.Operators;
using UniRx.InternalUtil;

public class FloatVariableVisualizer : MonoBehaviour
{
    [SerializeField] private FloatVariable _variable;

    [SerializeField] private Text _text;

    [SerializeField] private bool _useParamString;
    [SerializeField] private string _paramString;
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
