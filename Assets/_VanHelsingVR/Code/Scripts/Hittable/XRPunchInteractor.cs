using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;

public class XRPunchInteractor : XRDirectInteractor
{
    [SerializeField] private float fistClosedValue;
    [SerializeField] private InputActionProperty fistAction;

    public bool IsEntireClosed
    {
        get
        {
            var fistValue = fistAction.action.ReadValue<float>();
            return fistValue >= fistClosedValue;
        }
    }
}
