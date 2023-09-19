using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using _VanHelsingVR.Events;
using UnityEngine.Serialization;
using _VanHelsingVR.Variables;
using Sirenix.OdinInspector;

public abstract class Grabber : MonoBehaviour
{
    #region EVENTS

    [SerializeField] private ReactiveEvent<Grabbable> OnGrab;

    #endregion

#if UNITY_EDITOR
    [InlineProperty]
    #endif

    [SerializeField] protected ConditionPool canGrab;

    public Transform GrabOrigin => grabOrigin;
    [FormerlySerializedAs("originGrab")] 
    [SerializeField] 
    protected Transform grabOrigin;

    protected Grabbable currentGrabbable;
    
    protected void AddGrabbable(Grabbable grabbable)
    {
        if (currentGrabbable) return;
        currentGrabbable = grabbable;
    }
    
    protected void RemoveGrabbable(Grabbable grabbable)
    {
        if (!currentGrabbable) return;
        currentGrabbable = null;
    }
    
    protected virtual void TryGrab(Grabbable grabbable)
    {
        AddGrabbable(grabbable);
    }

    protected virtual void UnGrab(Grabbable grabbable)
    {
        RemoveGrabbable(grabbable);
    }
}
