using _VanHelsingVR.Health;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;

public abstract class Hittable: Damagable
{
    [SerializeField] protected float speedToBeHit = 5;
    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        var interactor = args.interactorObject.transform;
        if (!interactor.TryGetComponent(out XRPunchInteractor punch)) return;
        if (!punch.IsEntireClosed) return;
        if (!interactor.TryGetComponent(out SpeedoMeter speed)) return;
        
        //Know how many damage is done
        
        if (speed.Velocity.sqrMagnitude >= speedToBeHit - Mathf.Epsilon)
        {
            OnHit();
        }
        
        base.OnHoverEntered(args);
    }
}
