using System;
using RacTools.Variables;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace _VanHelsingVR.Player
{
    public class PlayerInput : MonoBehaviour
    {
        [field: SerializeField] public HandInput LeftHandInput { get; private set; } = new HandInput();
        [field: SerializeField] public HandInput RightHandInput { get; private set; } = new HandInput();
    
        void Update()
        {
            LeftHandInput.HandleInput();
            RightHandInput.HandleInput();
        }
    }

    [Serializable]
    public class HandInput
    {
        [SerializeField] private InputActionProperty selectAction;
        [SerializeField] private InputActionProperty activateAction;
    
        [FormerlySerializedAs("closedValue")] [SerializeField] private FloatReference actionValueToClosed;
        [SerializeField] private FloatReference triggerValueToClosed;

        public float SelectionInput { get; private set; }
        public float ActiveInput { get; private set; }

        public bool IsClosed => ActiveInput >= actionValueToClosed.Value && SelectionInput >= triggerValueToClosed.Value;
    
        public void HandleInput()
        {
            SelectionInput = selectAction.action.ReadValue<float>();
            ActiveInput = activateAction.action.ReadValue<float>();
        }
    }

    [Serializable]
    public class HandInputReference
    {
        [SerializeField] private HandOrientation handOrientation;
        [SerializeField] private PlayerInput playerInput;
    
        public HandInput Hand => handOrientation switch
        {
            HandOrientation.Left => playerInput.LeftHandInput,
            HandOrientation.Right => playerInput.RightHandInput,
            _ => throw new ArgumentOutOfRangeException()
        };
    
        public enum HandOrientation
        {
            Left,
            Right
        }
    }
}