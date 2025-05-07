using System;
using System.Collections.Generic;
using Autohand;
using UnityEngine;

namespace _VR_Helsing.Gun
{
    public class GunSelectionUI : MonoBehaviour
    {
        private static readonly List<GunSelectionUI> _SelectionMenus = new(2);

        [SerializeField] private CanvasGroup canvasGroup;
        
        [SerializeField] private List<GunSelectionSlotUI> slots;
        
        private Transform _followPoint;
        private bool _isBeingUsed;

        private void Start()
        {
            Deactivate();
        }

        public void Activate(Transform followTransform)
        {
            SetInitialConfig(followTransform);

            for (int i = 0; i < slots.Count; i++)
            {
                slots[i].gameObject.SetActive(true);
            }
        }

        private void SetInitialConfig(Transform followTransform)
        {
            _followPoint = followTransform;
        }

        public void Deactivate()
        {
            canvasGroup.alpha = 0;

            for (int i = 0; i < slots.Count; i++)
            {
                slots[i].gameObject.SetActive(false);
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
            _SelectionMenus.Add(this);
            GunSelectionSlotUI.OnSlotSelected += OnSlotSelected;
            GunSelectionSlotUI.OnSlotUnselected += OnSlotUnselected;
        }

        private void OnDisable()
        {
            GunSelectionSlotUI.OnSlotSelected -= OnSlotSelected;
            GunSelectionSlotUI.OnSlotUnselected -= OnSlotUnselected;
            _SelectionMenus.Remove(this);
        }

        #region Static Mehods
        public static bool GetSelection(out GunSelectionUI selection){
            selection = null;

            for (int i = 0; i < _SelectionMenus.Count; i++)
            {
                if(_SelectionMenus[i]._isBeingUsed || !_SelectionMenus[i].enabled) continue;

                selection = _SelectionMenus[i];
                selection._isBeingUsed = true;
            }

            return selection;
        }

        public static void ReturnSelection(GunSelectionUI selection){
            if(!selection) return;

            if(!_SelectionMenus.Contains(selection)) return;

            for (int i = 0; i < _SelectionMenus.Count; i++)
            {
                if(_SelectionMenus[i] == selection){
                    selection._isBeingUsed = false;
                }
            }
        }
        #endregion
    }
}
