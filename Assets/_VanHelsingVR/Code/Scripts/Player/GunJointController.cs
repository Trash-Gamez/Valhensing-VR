
using _VanHelsingVR.Variables;
using UnityEngine;

#if UNITY_EDITOR
using Sirenix.OdinInspector;
#endif

public class GunJointController : MonoBehaviour
{
    
#if UNITY_EDITOR
    [InlineProperty]
#endif
    [SerializeField]
    private Range jointLimitRange;

#if UNITY_EDITOR
    [Title("Gun Varibles"), Required, InlineProperty]
#endif
    
    [SerializeField]
    private FloatReference gunXSpeed;
    
#if UNITY_EDITOR
    [Title("Physics Stuff"), Required]
#endif
    
    [SerializeField] private HingeJoint gunJoint;

    private void FixedUpdate()
    {
        //TODO: Make the map for the {gunSpeed} for the gun pos;
    }
}
