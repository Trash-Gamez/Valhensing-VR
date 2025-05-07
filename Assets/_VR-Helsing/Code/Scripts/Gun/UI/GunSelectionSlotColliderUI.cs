using UnityEngine;

namespace _VR_Helsing.Gun
{
    [RequireComponent(typeof(SphereCollider))] //Debe estar configurado en eel editor para solo aceptar UI 
    public class GunSelectionSlotColliderUI : MonoBehaviour
    {
        [SerializeField] private GunSelectionSlotUI slot;
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out GunPointerUI gunPointer)) return;

            slot.Select(gunPointer);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent(out GunPointerUI gunPointer)) return;
            
            slot.Deselect(gunPointer);
        }
    }
}
