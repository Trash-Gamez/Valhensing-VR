using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class TextReference
{
    [SerializeField] private bool _useTMPro;

    [SerializeField] private Text _text;
    [SerializeField] private TMPro.TMP_Text _tmpText;

    public string Value
    {
        get
        {
            return _useTMPro ? _tmpText.text : _text.text;
            //TODO SET
        }
    }
}
