using UnityEngine;

namespace _VR_Helsing.Interactables.Door
{
    public class DoorLock : MonoBehaviour
    {
        public static event System.Action<DoorLock> OnLockDestroyed;

        public void Destroy()
        {
            OnLockDestroyed?.Invoke(this);
        }
    }
}