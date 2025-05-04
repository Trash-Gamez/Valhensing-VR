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
    
    //TODO: UI
    
    private GunDataHandler _gunDataHandler;
    private bool _isHolding;

    private void OnGunDataChanged(InputAction.CallbackContext ctx)
    {
        if (!_isHolding) return;
        
        _gunDataHandler.NextGun(1);
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
        onDataChange.action.performed += OnGunDataChanged;
    }

    private void OnDisable()
    {
        onDataChange.action.performed -= OnGunDataChanged;
        attachedHand.OnReleased -= OnReleased;
    }
}
