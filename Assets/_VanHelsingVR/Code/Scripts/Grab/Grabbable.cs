using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using _VanHelsingVR.Events;

#if UNITY_EDITOR
using Sirenix.OdinInspector;
#endif

[RequireComponent(typeof(SphereCollider))]
public class Grabbable : XRGrabInteractable
{
#if UNITY_EDITOR
    [Title("Grabbable Stuff")]
#endif
    [SerializeField] private ReactiveEvent<Grabbable> OnGrab;

    public string AnimName => animName;
    [SerializeField] private string animName;

    public void Grab(Grabber grabber)
    {
        attachTransform = grabber.GrabOrigin;
        OnGrab.Raise(this);
    }

    public void UnGrab()
    {
        attachTransform = null;
    }

}
