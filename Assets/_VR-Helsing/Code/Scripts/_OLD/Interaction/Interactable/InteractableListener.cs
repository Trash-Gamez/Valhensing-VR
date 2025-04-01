using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace _VanHelsingVR.Interaction
{
    [RequireComponent(typeof(UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable))]
    public abstract class InteractableListener : MonoBehaviour
    {
        protected UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable _interactor;
        
        protected virtual void Awake()
        {
            _interactor = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();
        }

        #region HOVER
        protected virtual void OnHoverEntered(HoverEnterEventArgs args){}
        protected virtual void OnHoverExit(HoverExitEventArgs args){}
        #endregion
        
        #region SELECT
        protected virtual void OnSelectEntered(SelectEnterEventArgs args){}
        protected virtual void OnSelectExit(SelectExitEventArgs args){}
        #endregion
        
        //Subscrbe to the events onEnable
        protected virtual void OnEnable()
        {
            _interactor.hoverEntered.AddListener(OnHoverEntered);
            _interactor.hoverExited.AddListener(OnHoverExit);
            _interactor.selectEntered.AddListener(OnSelectEntered);
            _interactor.selectExited.AddListener(OnSelectExit);
        }
        
        //Dispose the events onDisable
        protected virtual void OnDisable()
        {
            _interactor.hoverEntered.RemoveListener(OnHoverEntered);
            _interactor.hoverExited.RemoveListener(OnHoverExit);
            _interactor.selectEntered.RemoveListener(OnSelectEntered);
            _interactor.selectExited.RemoveListener(OnSelectExit);
        }
    }
}
