using _VanHelsingVR.Health;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;

public abstract class Hittable: XRSimpleInteractable
{
    [SerializeField] private DamagableHealthReference damagableHealthReference = null;
    [SerializeField] private float speedToBeHit = 5;
    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        var interactor = args.interactorObject.transform;
        if (!interactor.TryGetComponent(out XRPunchInteractor punch)) return;
        if (!punch.IsEntireClosed) return;
        if (!interactor.TryGetComponent(out SpeedoMeter speed)) return;
        
        //Know how many damage is done
        
        if (speed.Velocity.sqrMagnitude >= speedToBeHit - Mathf.Epsilon)
        {
            if(damagableHealthReference.HealthSystem != null)
                OnDamageHit();
            OnHit();
        }
        
        base.OnHoverEntered(args);
    }

    /// <summary>
    /// If there is a health system attached to this component, makes a damage control
    /// </summary>
    protected virtual void OnDamageHit(int damageDeal = 1)
    {
        damageDeal = Mathf.Abs(damageDeal);
        damagableHealthReference.HealthSystem.AddHealth(-damageDeal);
    }

    public abstract void OnHit();
}
