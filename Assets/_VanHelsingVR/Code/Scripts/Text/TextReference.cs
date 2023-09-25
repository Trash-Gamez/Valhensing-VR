using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class TextReference
{
    private enum TextType
    {
        Legacy,
        TMPro
    }
    
    [SerializeField] private TextType textType;

#if UNITY_EDITOR
    [ShowIf(nameof(textType), TextType.Legacy)]
#endif 
    [SerializeField]
    private Text legacyText;

#if UNITY_EDITOR
    [ShowIf(nameof(textType), TextType.TMPro)]
#endif 
    [SerializeField]
    private TMPro.TMP_Text tmpText;

    public string Value
    {
        get
        {
            return textType switch
            {
                TextType.Legacy => legacyText.text,
                TextType.TMPro => tmpText.text,
                _ => throw new Exception("TextType is not assigned")
            };
        }
        set
        {
            switch (textType)
            {
                case TextType.Legacy:
                    legacyText.text = value;
                    break;
                case TextType.TMPro:
                    tmpText.text = value;
                    break;
                default:
                    throw new Exception("TextType is not assigned");
            }
        }
    }
}
