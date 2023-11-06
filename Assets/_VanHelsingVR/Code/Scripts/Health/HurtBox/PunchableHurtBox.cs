using UnityEngine;
using _VanHelsingVR.Interaction;

using RacTools.Variables;

namespace _VanHelsingVR.Health
{
    public abstract class PunchableHurtBox : Hurtbox
    {
        [SerializeField] protected VariableReference<int> speedToBeHit;
        [SerializeField] protected VariableReference<bool> justPunch;
        
        //TODO: Saber diferencia entre punch y un hit
        /*
        protected override bool OnBeforeHit(Hitbox hitbox)
        {
            var interactor = hitbox.transform;
            
            if (!interactor.TryGetComponent(out XRLeftHandInteractor hand)) return !justPunch.Value;
            
            if (!hand.CanPunch) return;
            
            if (!interactor.TryGetComponent(out SpeedoMeter speed)) return;

            //Know how many damage is done

            if (speed.Velocity.sqrMagnitude >= speedToBeHit - Mathf.Epsilon)
            {
                //OnDamage();
            }
        }
        */
    }
}
