using UnityEngine;

namespace _VR_Helsing.Gun.Target
{
    [RequireComponent(typeof(Collider))]
    public class TargetableObject : MonoBehaviour, ITargetable
    {
        [SerializeField] private TargetProfile targetProfile;
        [SerializeField] private Transform centerTarget;
        
        public virtual TargetProfile TargetProfile => targetProfile;
        public Transform CenterTarget => centerTarget;
        public bool IsTargetable => _isTargetable;

        private bool _isTargetable = true;

        public void ChangeProfile(TargetProfile newProfile)
        {
            targetProfile = newProfile;
        }
    }
}