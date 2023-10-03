using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace _VanHelsingVR.Interaction
{
    [DefaultExecutionOrder(1)]
    public class DestroyOnActive : Grabbable
    {
        [Tooltip("The number of times the 'OnActivated' Event has to trigger for this object to be destroyed")]
        [Min(1),SerializeField] private int destroyAfter = 1;

        private int _timesTriggered = 0;
        protected override void OnActivated(ActivateEventArgs args)
        {
            if(++_timesTriggered >= destroyAfter)
                Destroy(gameObject);
        }
    }
}