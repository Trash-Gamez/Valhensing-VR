using System;
using System.Collections;
using System.Collections.Generic;
using _VanHelsingVR.Text;
using _VR_Helsing.Gun;
using MEC;
using UnityEngine;

public abstract class TEST_VarGunText : MonoBehaviour
{
    public const float TIME_STAMP = 0.005f;
    private static readonly List<TEST_VarGunText> _Texts = new();
    
    [SerializeField] protected GunStateMachine gun;
    [SerializeField] protected TextReference text;

    protected abstract string GetText();

    private static IEnumerator<float> SetText()
    {
        while (true)
        {
            for (int i = 0; i < _Texts.Count; i++)
            {
                _Texts[i].text.Value = _Texts[i].GetText();
            }

            yield return Timing.WaitForSeconds(TIME_STAMP);
        }
    }

    [RuntimeInitializeOnLoadMethod]
    private static void InitCoroutine()
    {
        Timing.RunCoroutine(SetText());
    }
    
    private void OnEnable()
    {
        _Texts.Add(this);
    }

    private void OnDisable()
    {
        _Texts.Remove(this);
    }
}
