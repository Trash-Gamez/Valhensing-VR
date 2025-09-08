using UnityEngine;

namespace _VR_Helsing.Gun.Target
{
    [RequireComponent(typeof(Collider))]
    public class TargetableObject : MonoBehaviour, ITargetable
    {
        [SerializeField] private TargetProfile targetProfile;
        
        public virtual TargetProfile TargetProfile => targetProfile;
        public bool IsTargetable => _isTargetable;

        private bool _isTargetable = true;

        public void ChangeProfile(TargetProfile newProfile)
        {
            targetProfile = newProfile;
        }
    }
}