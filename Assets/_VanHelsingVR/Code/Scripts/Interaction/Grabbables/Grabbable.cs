using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace _VanHelsingVR.Interaction
{
    [RequireComponent(typeof(XRGrabInteractable))]
    public abstract class Grabbable : Interactable
    {
        protected XRGrabInteractable _grabbable;

        protected override void Awake()
        {
            base.Awake();
            _grabbable = GetComponent<XRGrabInteractable>();
        }
        
        #region FOCUS
        protected virtual void OnFocusEntered(FocusEnterEventArgs args){}
        protected virtual void OnFocusExit(FocusExitEventArgs args){}
        #endregion
        
        #region ACTIVATE
        protected virtual void OnActivated(ActivateEventArgs args){}
        protected virtual void OnDeactivated(DeactivateEventArgs args){}
        #endregion

        protected override void OnEnable()
        {
            base.OnEnable();
            _grabbable.activated.AddListener(OnActivated);
            _grabbable.deactivated.AddListener(OnDeactivated);
            _grabbable.focusEntered.AddListener(OnFocusEntered);
            _grabbable.focusExited.AddListener(OnFocusExit);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _grabbable.activated.RemoveListener(OnActivated);
            _grabbable.deactivated.RemoveListener(OnDeactivated);
            _grabbable.focusEntered.RemoveListener(OnFocusEntered);
            _grabbable.focusExited.RemoveListener(OnFocusExit);
        }
    }
}