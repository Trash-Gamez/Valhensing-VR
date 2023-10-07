using _VanHelsingVR.Health;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;

namespace _VanHelsingVR.Interaction
{
    public abstract class Hittable : Damagable
    {
        [SerializeField] protected float speedToBeHit = 5;

        protected override void OnHoverEntered(HoverEnterEventArgs args)
        {
            Debug.Log("Se paro encima mio");
            var interactor = args.interactorObject.transform;
            if (!interactor.TryGetComponent(out XRLeftHandInteractor hand)) return;
            Debug.Log("Si hay mano izquierda");
            if (!hand.CanPunch) return;
            Debug.Log("Puede Pegar");
            if (!interactor.TryGetComponent(out SpeedoMeter speed)) return;

            //Know how many damage is done

            if (speed.Velocity.sqrMagnitude >= speedToBeHit - Mathf.Epsilon)
            {
                OnDamage();
            }

            base.OnHoverEntered(args);
        }
    }
}
