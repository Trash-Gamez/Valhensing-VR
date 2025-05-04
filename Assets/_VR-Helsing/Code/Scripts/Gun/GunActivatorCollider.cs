using Autohand;
using UnityEngine;
using UnityEngine.Serialization;

namespace _VR_Helsing.Gun
{
    public class GunActivatorCollider : MonoBehaviour
    {
        [SerializeField] private NEW_GunHolder gunHolder;
        [SerializeField] private string handTag;
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(handTag)) return;
            if (!other.TryGetComponent(out Hand hand)) return;
            
            gunHolder.AlterGun(hand);
        }
    }
}
