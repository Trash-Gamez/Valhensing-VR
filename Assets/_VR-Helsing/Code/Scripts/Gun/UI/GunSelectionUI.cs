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
        
        [SerializeField] private float positionSmoothTime = 0.15f; // Puedes ajustar este valor en el Inspector
        
        [Tooltip("Velocidad máxima a la que el menú puede moverse. Mathf.Infinity para sin límite.")]
        [SerializeField] private float maxMoveSpeed = Mathf.Infinity;
        
        private Vector3 _currentPositionVelocity = Vector3.zero;
        
        private Transform _followPoint;
        private bool _isBeingUsed;

        private Transform _fromOffset;
        private Vector3 _neededOffset;
        
        private int _slotsSelected;

        private GunSelectionSlotUI _currentSlot;
        public int SlotGunIndex => _currentSlot.GunIndex;

        private void Start()
        {
            Hide();
        }

        private void LateUpdate()
        {
            if (!_isBeingUsed) return;
            transform.position = Vector3.SmoothDamp(
                transform.position,
                _fromOffset.position + _neededOffset,
                ref _currentPositionVelocity,
                positionSmoothTime,
                maxMoveSpeed,
                Time.deltaTime);
        }

        public void Show(Vector3 initialPos, Transform offsetFrom, Transform initialRotation)
        {
            SetInitialConfig(initialPos,offsetFrom, initialRotation);

            _slotsSelected = 0;
            canvasGroup.alpha = 1;
            
            for (int i = 0; i < slots.Count; i++)
            {
                slots[i].gameObject.SetActive(true);
            }
        }

        private void SetInitialConfig(Vector3 initialPos, Transform offsetFrom, Transform initialRotation)
        {
            _fromOffset = offsetFrom;
            transform.position = initialPos;

            //var fromOffset = _fromOffset.position;
            //fromOffset.y = initialPos.y;
            _neededOffset = initialPos -  _fromOffset.position;
            
            
            //transform.rotation = Quaternion.LookRotation(fromOffset.normalized, Vector3.up);
            transform.rotation = initialRotation.rotation;
        }

        public void Hide()
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

            _slotsSelected--;
            if (_slotsSelected <= 0)
            {
                _slotsSelected = 0;
                _currentSlot = null;
            }
        }

        private void OnSlotSelected(GunSelectionSlotUI slot)
        {
            if (!slots.Contains(slot)) return;

            _slotsSelected++;
            _currentSlot = slot;
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
                if(_SelectionMenus[i]._isBeingUsed) continue;

                selection = _SelectionMenus[i];
                selection._isBeingUsed = true;
                break;
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
