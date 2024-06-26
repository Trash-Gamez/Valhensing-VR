using UnityEngine;

using RacTools.Variables;
using UnityEngine.Events;

namespace _VanHelsingVR.Health
{
    
    public abstract class PunchableHurtBox : Hurtbox
    {
        [SerializeField] protected VariableReference<int> speedToBeHit;
        [SerializeField] private UnityEvent onPunch;

        protected override bool OnBeforePunch(PunchableHitbox punchable)
        {
            var interactor = punchable.transform;
           
            if (!interactor.TryGetComponent(out SpeedoMeter speed)) return false;

            if (speed.Velocity.sqrMagnitude >= speedToBeHit.Value - Mathf.Epsilon)
            {
                onPunch?.Invoke();
                return true;
            }

            return false;
        }
    }
}
