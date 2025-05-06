using System;
using UnityEngine;

namespace _VR_Helsing.Gun
{
    public class GunSelectionSlotUI : MonoBehaviour
    {
        public static event Action<GunSelectionSlotUI> OnSlotSelected;
        public static event Action<GunSelectionSlotUI> OnSlotUnselected;

        public GunPointerUI Pointer => _gunPointer;
        
        private GunPointerUI _gunPointer;
        private bool _isTouching;
        
        public void Select(GunPointerUI gunPointer)
        {
            if (!enabled) return;
            if (!_isTouching) return;

            _gunPointer = gunPointer;
            _isTouching = true;
            
            OnSlotSelected?.Invoke(this);
        }

        public void Deselect(GunPointerUI gunPointer)
        {
            if (!enabled) return;
            if (!_isTouching) return;

            if (gunPointer != _gunPointer) return;
            _isTouching = false;
            _gunPointer = null;
            
            OnSlotUnselected?.Invoke(this);
        }
    }
}