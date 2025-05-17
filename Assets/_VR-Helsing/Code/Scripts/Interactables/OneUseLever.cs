using System;
using Autohand;
using UnityEngine;
using UnityEngine.Events;

public class OneUseLever : MonoBehaviour
{
    [SerializeField] private PhysicsGadgetLever gadget;

    [SerializeField] private Grabbable grabbable;
    [SerializeField] private UnityEvent OnActivate;

    public void Activate()
    {
        OnActivate?.Invoke();
        grabbable.enabled = false;
    }
}
