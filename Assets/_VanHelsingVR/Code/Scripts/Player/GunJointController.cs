
using _VanHelsingVR.Variables;
using UnityEngine;

#if UNITY_EDITOR
using Sirenix.OdinInspector;
#endif

public class GunJointController : MonoBehaviour
{
    [System.Flags]
    private enum WaysToDoIt{
        WithSpringJoint,
        WithLimits
    }

    #if UNITY_EDITOR
    [Title("Way To Do it")]
    #endif
    
    [SerializeField]
    private WaysToDoIt wayToDoIt;

    #if UNITY_EDITOR
    [Title("Gun Varibles"), Required, InlineProperty]
    #endif
    
    [SerializeField]
    private FloatReference gunXSpeed;
    
    
    #if UNITY_EDITOR
    [InlineProperty]
    [PropertySpace]
    #endif
    [SerializeField]
    private Range jointLimitRange;
    
    #if UNITY_EDITOR
    [Title("Physics Stuff"), Required]
    #endif
    
    [SerializeField] private HingeJoint gunJoint;

    private void FixedUpdate()
    {
        //TODO: Make the map for the {gunSpeed} for the gun pos;
    }
}
