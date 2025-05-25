using System;
using _VR_Helsing.Gun;
using Autohand;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunDataChanger : MonoBehaviour
{
    [Header("Hand")]
    [SerializeField] private Hand attachedHand;
    
    [Header("Input")]
    [SerializeField] private InputActionProperty onDataChange;

    [Header("Params")]
    [SerializeField] private float longPressSeconds = 0.5f;

    [SerializeField] private Transform showMenuPos;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform cameraTransform;

    private float _seconds;
    private GunSelectionUI _selectionUI;
    
    //TODO: UI
    
    private GunDataHandler _gunDataHandler;
    private bool _isHolding, _uiActive, _inputPerformed;

    private void SetActiveUI(bool isActive)
    {
        _uiActive = isActive;

        if (isActive)
        {
            GunSelectionUI.GetSelection(out _selectionUI);
            _selectionUI.Show(showMenuPos.position, playerTransform, cameraTransform);
        }
        else
        {
            if (!_selectionUI) return;

            if (_selectionUI.SlotGunIndex >= 0)
            {
                _gunDataHandler.SetGun(_selectionUI.SlotGunIndex);
            }
            
            GunSelectionUI.ReturnSelection(_selectionUI);
            _selectionUI.Hide();
            
        }
    }

    private void Update()
    {
        if(!_inputPerformed) return;
        if(_uiActive) return;

        _seconds += Time.deltaTime;

        if(_seconds >= longPressSeconds){
            LongPress();
        }
    }

    private void LongPress()
    {
        if(_uiActive) return;
        if (!_isHolding) return;

        SetActiveUI(true);
    }

    private void ShortPress(){
        if (!_isHolding) return;
        
        _gunDataHandler.NextGun(1);
    }

    private void OnGunDataPerfomed(InputAction.CallbackContext ctx)
    {
        _uiActive = false;
        _seconds = 0;
        _inputPerformed = true;
    }

    private void OngunDataCanceled(InputAction.CallbackContext ctx)
    {
        if(_uiActive)
        {
            SetActiveUI(false);
        }
        else
        {
            ShortPress();
        }

        _inputPerformed = false;
    }

    private void OnGrabbed(Hand hand, Grabbable grabbable)
    {
        if (!grabbable.CompareTag("Gun")) return;
        if (!grabbable.TryGetComponent(out _gunDataHandler)) return;

        _isHolding = true;
    }
    
    private void OnReleased(Hand hand, Grabbable grabbable)
    {
        if (!grabbable.CompareTag("Gun")) return;
        if (!grabbable.TryGetComponent(out GunDataHandler newDataHandler)) return;
        if (newDataHandler != _gunDataHandler) return;

        _isHolding = false;
        _gunDataHandler = null;
    }
    
    private void OnEnable()
    {
        attachedHand.OnGrabbed += OnGrabbed;
        attachedHand.OnReleased += OnReleased;
        
        onDataChange.action.Enable();
        onDataChange.action.performed += OnGunDataPerfomed;
        onDataChange.action.canceled += OngunDataCanceled;
    }

    private void OnDisable()
    {
        onDataChange.action.performed -= OnGunDataPerfomed;
        onDataChange.action.canceled -= OngunDataCanceled;

        attachedHand.OnGrabbed -= OnGrabbed;
        attachedHand.OnReleased -= OnReleased;
    }
}
