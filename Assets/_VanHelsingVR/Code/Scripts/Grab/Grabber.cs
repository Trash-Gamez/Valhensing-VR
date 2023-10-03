using UnityEngine;

using _VanHelsingVR.Events;
using UnityEngine.Serialization;

#if UNITY_EDITOR
using Sirenix.OdinInspector;
#endif

public abstract class Grabber : MonoBehaviour
{
    #region EVENTS

    [SerializeField] private ReactiveEvent<Grabbable> OnGrab;

    #endregion

    #if UNITY_EDITOR
    [InlineProperty]
    #endif
    [SerializeField] protected ConditionPool canGrab;
    
    #if UNITY_EDITOR
    [InlineProperty]
    #endif
    [SerializeField] protected ConditionPool canUnGrab;

    public Transform GrabOrigin => grabOrigin;
    [FormerlySerializedAs("originGrab")] 
    [SerializeField] 
    protected Transform grabOrigin;

    protected Grabbable currentGrabbable;
    
    protected void AddGrabbable(Grabbable grabbable)
    {
        if (currentGrabbable) return;
        currentGrabbable = grabbable;
        currentGrabbable.Grab(this);
    }
    
    protected void RemoveGrabbable(Grabbable grabbable)
    {
        if (!currentGrabbable) return;
        if (currentGrabbable != grabbable) return;
        
        currentGrabbable.UnGrab();
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
