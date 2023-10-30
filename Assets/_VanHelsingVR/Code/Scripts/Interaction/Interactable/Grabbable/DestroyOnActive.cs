using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;

namespace _VanHelsingVR.Interaction
{
    [DefaultExecutionOrder(1)]
    public class DestroyOnActive : GrabbableListener
    {
        [Tooltip("The number of times the 'OnActivated' Event has to trigger for this object to be destroyed")]
        [Min(1),SerializeField]
        [FormerlySerializedAs("Destroy After i Timer")]
        private int destroyAfter = 1;

        private int _timesTriggered = 0;
        protected override void OnActivated(ActivateEventArgs args)
        {
            if(++_timesTriggered >= destroyAfter)
            Destroy(gameObject);
        }
    }
}