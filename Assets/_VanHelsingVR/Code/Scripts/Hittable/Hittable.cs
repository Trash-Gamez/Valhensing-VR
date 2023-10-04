using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public abstract class Hittable: XRSimpleInteractable
{
    [SerializeField] private float speedToBeHit = 5;
    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        if (!args.interactorObject.transform.TryGetComponent(out XRPunchInteractor punch)) return;
        if (!punch.IsEntireClosed) return;
        if (!args.interactorObject.transform.TryGetComponent(out Rigidbody rb)) return;
        
        Debug.Log(rb.velocity.sqrMagnitude);
        if(rb.velocity.sqrMagnitude >= speedToBeHit - Mathf.Epsilon)
            base.OnHoverEntered(args);
    }

    public abstract void OnHit();
}
