using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace _VR_Helsing.Interactables.Door
{
    public class LockedBigDoor : MonoBehaviour
    {
        [SerializeField] private List<DoorLock> locks;
        [SerializeField] private UnityEvent onDoorOpen;

        private void OnLockDestroyed(DoorLock doorLock)
        {
            if (!locks.Contains(doorLock)) return;
            
            locks.Remove(doorLock);

            if (locks.Count > 0) return;
            
            onDoorOpen?.Invoke();
        }
        
        private void OnEnable() => DoorLock.OnLockDestroyed += OnLockDestroyed;

        private void OnDisable() => DoorLock.OnLockDestroyed -= OnLockDestroyed;
    }
}
