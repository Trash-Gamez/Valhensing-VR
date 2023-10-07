using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace _VanHelsingVR.Interaction
{
    public class ParticlesOnActive : GrabbableListener
    {
        [SerializeField] private ParticleSystem partycles;

        [SerializeField] private bool pauseOnDeactive;
        protected override void OnActivated(ActivateEventArgs args)
        {
            partycles.Play();
        }

        protected override void OnDeactivated(DeactivateEventArgs args)
        {
            if(pauseOnDeactive)
                partycles.Pause();
        }
    }
}