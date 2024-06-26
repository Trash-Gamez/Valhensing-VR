using _VanHelsingVR.Utilities;
using RacTools.Utilities;
using RacTools.Variables;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _VanHelsingVR.Player
{
    public class GunJointController : MonoBehaviour
    {
        [System.Flags]
        private enum WaysToDoIt{
            WithJointSpring = 1,
            WithJointLimits = 2,
            All = WithJointLimits | WithJointSpring
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
        [InlineProperty]
        [PropertySpace]
#endif
        [SerializeField]
        private Range speedRange;

#if UNITY_EDITOR
        [Title("Physics Stuff"), Required]
#endif
    
        [SerializeField] private HingeJoint gunJoint;

        private void FixedUpdate()
        {
            var limits = gunJoint.limits;
            limits.max = UtilitieExtensions.Map(gunXSpeed.Value, speedRange, jointLimitRange);
            gunJoint.limits = limits;

            //TODO: SMOOTH!!!
        }
    }
}
