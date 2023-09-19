using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using _VanHelsingVR.Events;
using UnityEngine.Serialization;

public abstract class Grabber : MonoBehaviour
{
    #region EVENTS

    [SerializeField] private ReactiveEvent<Grabbable> OnGrab;
    
    #endregion

    
    
    [FormerlySerializedAs("originGrab")] [SerializeField] protected Transform grabOrigin;
    protected List<Grabbable> currentGrabbables = new();
    
    protected void AddGrabbable(Grabbable grabbable)
    {
        if (currentGrabbables.Contains(grabbable)) return;
        currentGrabbables.Add(grabbable);
    }
    
    protected void RemoveGrabbable(Grabbable grabbable)
    {
        if (!currentGrabbables.Contains(grabbable)) return;
        currentGrabbables.Remove(grabbable);
    }
    
    protected abstract void TryGrab(Grabbable grabbable);

    protected abstract void UnGrab(Grabbable grabbable);
}
