using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

using UnityEditor.Events;

namespace _VanHelsingVR.Interaction
{
    [RequireComponent(typeof(XRSimpleInteractable))]
    public class Button : MonoBehaviour
    {
        public UnityEvent OnClickButton;
        
        public void OnClick(SelectEnterEventArgs args)
        {
            OnClickButton?.Invoke();
        }
        
        [ContextMenu("Add to Simpleinteractable")]
        public void AddToEvent()
        {
            var simpleInteractable = GetComponent<XRSimpleInteractable>();
            UnityEventTools.AddPersistentListener(simpleInteractable.selectEntered, OnClick);
        }
    }
}
