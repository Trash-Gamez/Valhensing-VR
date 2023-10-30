using _VanHelsingVR.Health;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;

namespace _VanHelsingVR.Interaction
{
    public class Damagable : XRSimpleInteractable
    {
        [SerializeField] protected DamagableHealthReference damagableHealthReference = null;

        public UnityEvent OnHit => onHit;
        [FormerlySerializedAs("OnHitEvent")] [SerializeField] private UnityEvent onHit;
    
        public virtual void OnDamage()
        {
            if(damagableHealthReference.HealthSystem != null)
                HandleDamage();
        
            OnHit?.Invoke();
        }
    
        /// <summary>
        /// If there is a health system attached to this component, makes a damage control
        /// </summary>
        protected virtual void HandleDamage(int damageDeal = 1)
        {
            damageDeal = Mathf.Abs(damageDeal);
            damagableHealthReference.HealthSystem.AddHealth(-damageDeal);
        }
    
#if UNITY_EDITOR
        [ContextMenu("Force Damage")]
        public void ForceHandleDamage()
        {
            OnDamage();
        }
#endif
    }
}
