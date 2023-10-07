using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace _VanHelsingVR.Interaction
{
    public class Grabbable : XRGrabInteractable
    {
        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            if (!args.interactorObject.transform.TryGetComponent(out XRLeftHandInteractor leftHand)) return;
            Debug.Log("Si hy mano izquierda");
            leftHand.Grab(this);
            base.OnSelectEntered(args);
        }

        protected override void OnSelectExited(SelectExitEventArgs args)
        {
            if (args.interactorObject.transform.TryGetComponent(out XRLeftHandInteractor leftHand))
            {
                leftHand.Ungrab(this);
            }

            base.OnSelectExited(args);
        }
    }
}
