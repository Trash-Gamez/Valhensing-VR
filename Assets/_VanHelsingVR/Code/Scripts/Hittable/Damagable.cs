using _VanHelsingVR.Health;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public abstract class Damagable : XRSimpleInteractable
{
    [SerializeField] protected DamagableHealthReference damagableHealthReference = null;

    [SerializeField] private UnityEvent OnHitEvent;
    
    public virtual void OnHit()
    {
        if(damagableHealthReference.HealthSystem != null)
            HandleDamage();
        
        OnHitEvent?.Invoke();
    }
    
    /// <summary>
    /// If there is a health system attached to this component, makes a damage control
    /// </summary>
    protected virtual void HandleDamage(int damageDeal = 1)
    {
        damageDeal = Mathf.Abs(damageDeal);
        damagableHealthReference.HealthSystem.AddHealth(-damageDeal);
    }
}
