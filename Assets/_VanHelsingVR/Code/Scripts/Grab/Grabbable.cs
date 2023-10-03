using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using _VanHelsingVR.Events;

#if UNITY_EDITOR
using Sirenix.OdinInspector;
#endif

[RequireComponent(typeof(SphereCollider))]
public class Grabbable : MonoBehaviour
{
#if UNITY_EDITOR
    [Title("Grabbable Stuff")]
#endif
    [SerializeField] private ReactiveEvent<Grabbable> OnGrab;

    public string AnimName => animName;
    [SerializeField] private string animName;

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Grab(Grabber grabber)
    {
        if(_rb != null && !_rb.IsSleeping()) _rb.Sleep();
        transform.position = grabber.GrabOrigin.position;
        transform.parent = grabber.GrabOrigin;
        
        Debug.Log($"Ahora estoy attached a: {grabber.name}");
        if(OnGrab != null)
            OnGrab.Raise(this);
    }

    public void UnGrab()
    {
        if (_rb != null && _rb.IsSleeping()) _rb.WakeUp();
        transform.parent = null;
    }

}
