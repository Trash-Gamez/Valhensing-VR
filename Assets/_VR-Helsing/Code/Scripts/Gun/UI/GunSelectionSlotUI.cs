using System;
using UnityEngine;
using UnityEngine.UI;

namespace _VR_Helsing.Gun
{
    public class GunSelectionSlotUI : MonoBehaviour
    {
        public static event Action<GunSelectionSlotUI> OnSlotSelected;
        public static event Action<GunSelectionSlotUI> OnSlotUnselected;

        [SerializeField] private Image highlightImage;
        [SerializeField] private Image gunImage;
        [SerializeField] private Sprite gunSprite;
        [SerializeField] private int gunIndex;
        public int GunIndex => gunIndex;

        private void OnEnable()
        {   
            highlightImage.color = Color.clear;
            gunImage.sprite = gunSprite;
            _isTouching = false;
        }

        public GunPointerUI Pointer => _gunPointer;
        
        private GunPointerUI _gunPointer;
        private bool _isTouching;
        
        public void Select(GunPointerUI gunPointer)
        {
            if (!enabled) return;
            if (_isTouching) return;
            
            _gunPointer = gunPointer;
            _isTouching = true;
            
            highlightImage.color = Color.white;
            
            OnSlotSelected?.Invoke(this);
        }

        public void Deselect(GunPointerUI gunPointer)
        {
            if (!enabled) return;
            if (!_isTouching) return;

            if (gunPointer != _gunPointer) return;
            _isTouching = false;
            _gunPointer = null;
            
            highlightImage.color = Color.clear;
            
            OnSlotUnselected?.Invoke(this);
        }

        private void OnValidate()
        {
            if(gunImage) gunImage.sprite = gunSprite;
        }
    }
}