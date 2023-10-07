using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace _VanHelsingVR.Interaction
{
    [RequireComponent(typeof(Rigidbody))]
    public class Grabbable : XRGrabInteractable
    {
        private Rigidbody _rb;
        protected override void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.useGravity = false;
            base.Awake();
        }

        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            if (!args.interactorObject.transform.TryGetComponent(out XRLeftHandInteractor leftHand)) return;
            leftHand.Grab(this);
            base.OnSelectEntered(args);
        }

        protected override void OnSelectExited(SelectExitEventArgs args)
        {
            if (args.interactorObject.transform.TryGetComponent(out XRLeftHandInteractor leftHand))
            {
                leftHand.Ungrab(this);
            }

            _rb.useGravity = true;
            base.OnSelectExited(args);
        }
    }
}
