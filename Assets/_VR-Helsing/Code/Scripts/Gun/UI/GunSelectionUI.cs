using System;
using System.Collections.Generic;
using Autohand;
using UnityEngine;

namespace _VR_Helsing.Gun
{
    public class GunSelectionUI : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Transform pointToFollow;
        [SerializeField] private List<GunSelectionSlotUI> slots;
        
        private Hand _hand;

        private void Start()
        {
            Deactivate();
        }

        public bool Activate(Hand hand)
        {
            if (_hand) return false;
            
            for (int i = 0; i < slots.Count; i++)
            {
                slots[i].enabled = true;
            }
            
            return true;
        }

        public void Deactivate()
        {
            canvasGroup.alpha = 0;
            _hand = null;

            for (int i = 0; i < slots.Count; i++)
            {
                slots[i].enabled = false;
            }
        }

        private void OnSlotUnselected(GunSelectionSlotUI slot)
        {
            if (!slots.Contains(slot)) return;
        }

        private void OnSlotSelected(GunSelectionSlotUI slot)
        {
            if (!slots.Contains(slot)) return;
        }
        
        private void Update()
        {
        
        }

        private void LateUpdate()
        {
            //transform.position = pointToFollow.position;
        }

        private void OnEnable()
        {
            GunSelectionSlotUI.OnSlotSelected += OnSlotSelected;
            GunSelectionSlotUI.OnSlotUnselected += OnSlotUnselected;
        }

        private void OnDisable()
        {
            GunSelectionSlotUI.OnSlotSelected -= OnSlotSelected;
            GunSelectionSlotUI.OnSlotUnselected -= OnSlotUnselected;
        }
    }
}
