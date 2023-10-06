using _VanHelsingVR.Variables;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class XRLeftHandInteractor : XRDirectInteractor
{
    [SerializeField, Range(0.1f, 1)] private float fistClosedValue;
    [SerializeField] private InputActionProperty fistAction;
    [SerializeField] private Variable<bool> isGrabbing;

    public bool IsEntireClosed
    {
        get
        {
            if (isGrabbing) return false;
            //TODO: get to know individually and not in this function if is grabbing something 
            var fistValue = fistAction.action.ReadValue<float>();
            return fistValue >= fistClosedValue;
        }
    }

    public void Grab(Grabbable grabbable)
    {
        isGrabbing.Value = true;
        Debug.Log("La mano izquierda agarro");
    }

    public void Ungrab(Grabbable grabbable)
    {
        Debug.Log("La mano izquierda dejo");
        isGrabbing.Value = false;
    }
}
