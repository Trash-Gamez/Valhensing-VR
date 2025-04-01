using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

#if UNITY_EDITOR
using UnityEditor.Events;
#endif

namespace _VanHelsingVR.Interaction
{
    [RequireComponent(typeof(UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable))]
    public class Button : MonoBehaviour
    {
        public UnityEvent OnClickButton;
        
        public void OnClick(SelectEnterEventArgs args)
        {
            OnClickButton?.Invoke();
        }

        #if UNITY_EDITOR
        [ContextMenu("Add to Simpleinteractable")]
        public void AddToEvent()
        {
            var simpleInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();
            UnityEventTools.AddPersistentListener(simpleInteractable.selectEntered, OnClick);
        }
        #endif
    }
}
