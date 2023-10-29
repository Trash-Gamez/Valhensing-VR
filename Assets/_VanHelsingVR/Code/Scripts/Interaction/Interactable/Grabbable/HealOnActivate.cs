using _VanHelsingVR.Events;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace _VanHelsingVR.Interaction
{
    public class HealOnActivate : GrabbableListener
    {
        [SerializeField] private ReactiveEvent<int> OnHealPlayer;
        [SerializeField] private GameObject blood;
        protected override void OnActivated(ActivateEventArgs args)
        {
            //TODO: HEAL_PLAYER ON ACTIVE
          
            AudioManager.Instance.PlaySound3D("HeartExplotion", transform.position);
            OnHealPlayer.Raise(1);
        }
    }
}