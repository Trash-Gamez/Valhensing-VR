using _VanHelsingVR.Events;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace _VanHelsingVR.Interaction
{
    public class HealOnActivate : GrabbableListener
    {
        [SerializeField] private int lifeToHeal;
        [SerializeField] private ReactiveEvent<int> OnHealPlayer;
        
        protected override void OnActivated(ActivateEventArgs args)
        {
            //TODO: HEAL_PLAYER ON ACTIVE          
            OnHealPlayer.Raise(lifeToHeal);
        }
    }
}