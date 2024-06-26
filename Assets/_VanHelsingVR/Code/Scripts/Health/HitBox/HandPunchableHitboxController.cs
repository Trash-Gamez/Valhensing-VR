using System;
using System.Collections;
using System.Collections.Generic;
using Autohand;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandPunchableHitboxController : MonoBehaviour
{
    [SerializeField] private Hand hand;
    [SerializeField] private Collider punchCollider;

    [Title("Input")] 
    [SerializeField] private InputActionProperty leftHandGrip;
    [SerializeField] private InputActionProperty leftHandAction;
    
    [SerializeField] private InputActionProperty rightHandGrip;
    [SerializeField] private InputActionProperty rightHandAction;
    
    [Range(0,1)]
    [SerializeField] private float valueToCountAsActive = .85f;

    private bool _isGripping;
    private bool _isActioning;
    
    private void Start()
    {
        punchCollider.enabled = false;
    }

    private void UpdateHandStatus()
    {
        punchCollider.enabled = !hand.IsHolding() && _isActioning && _isGripping;

        if (punchCollider.enabled)
        {
            Debug.Log("No tiene nada, esta actionando y esta grippeando");
        }
        else
        {
            Debug.Log("No esta encendido");
        }
        
    }

    private void LeftHandAction(InputAction.CallbackContext ctx)
    {
        _isActioning = !ctx.canceled && ctx.ReadValue<float>() > valueToCountAsActive;
        UpdateHandStatus();
    }
    
    private void LeftHandGrip(InputAction.CallbackContext ctx)
    {
        _isGripping = !ctx.canceled && ctx.ReadValue<float>() > valueToCountAsActive;
        UpdateHandStatus();
    }
    
    private void RightHandAction(InputAction.CallbackContext ctx)
    {
        _isActioning = !ctx.canceled && ctx.ReadValue<float>() > valueToCountAsActive;
        UpdateHandStatus();
    }
    private void RightHandGrip(InputAction.CallbackContext ctx)
    {
        _isGripping = !ctx.canceled && ctx.ReadValue<float>() > valueToCountAsActive;
        UpdateHandStatus();
    }
    
    private void OnEnable()
    {
        if (hand.left)
        {
            leftHandGrip.action.Enable();
            leftHandAction.action.Enable();
            
            leftHandAction.action.started += LeftHandAction;
            leftHandAction.action.performed += LeftHandAction;
            leftHandAction.action.canceled += LeftHandAction;
            
            leftHandGrip.action.started += LeftHandGrip;
            leftHandGrip.action.performed += LeftHandGrip;
            leftHandGrip.action.canceled += LeftHandGrip;
        }
        else
        {
            rightHandAction.action.started += RightHandAction;
            rightHandAction.action.performed += RightHandAction;
            rightHandAction.action.canceled += RightHandAction;
            
            rightHandGrip.action.started += RightHandGrip;
            rightHandGrip.action.performed += RightHandGrip;
            rightHandGrip.action.canceled += RightHandGrip;
        }
    }


    private void OnDisable()
    {
        if (hand.left)
        {
            leftHandAction.action.started -= LeftHandAction;
            leftHandAction.action.performed -= LeftHandAction;
            leftHandAction.action.canceled -= LeftHandAction;
            leftHandGrip.action.started -= LeftHandGrip;
            leftHandGrip.action.performed -= LeftHandGrip;
            leftHandGrip.action.canceled -= LeftHandGrip;
        }
        else
        {
            rightHandAction.action.started -= RightHandAction;
            rightHandAction.action.performed -= RightHandAction;
            rightHandAction.action.canceled -= RightHandAction;
            rightHandGrip.action.started -= RightHandGrip;
            rightHandGrip.action.performed -= RightHandGrip;
            rightHandGrip.action.canceled -= RightHandGrip;
        }
    }
}
