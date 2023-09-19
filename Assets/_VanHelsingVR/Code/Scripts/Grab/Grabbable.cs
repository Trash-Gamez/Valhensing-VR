using _VanHelsingVR.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(SphereCollider))]
public class Grabbable : XRGrabInteractable
{
    [SerializeField] private ReactiveEvent<Grabbable> OnGrab;

    public string AnimName => animName;
    [SerializeField] private string animName;

    public void Grab(Grabber grabber)
    {
        attachTransform = grabber.GrabOrigin;
        OnGrab.Raise(this);
    }

    public void UnGrab()
    {
        attachTransform = null;
    }

}
