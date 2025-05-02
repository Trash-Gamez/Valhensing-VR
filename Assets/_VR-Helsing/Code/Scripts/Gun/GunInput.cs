using Autohand;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _VR_Helsing.Gun
{
    [DefaultExecutionOrder(-1)]
    public class GunInput : MonoBehaviour
    {
        
        #if UNITY_EDITOR
        [Title("Grabbable")] 
        #endif
        [SerializeField] private Grabbable gunGrabbable;
        
        #if UNITY_EDITOR
        [Title("Input Actions")] 
        #endif
        [SerializeField] private InputActionProperty leftTriggerAction;
        [SerializeField] private InputActionProperty leftGripAction;
        [SerializeField] private InputActionProperty rightTriggerAction;
        [SerializeField] private InputActionProperty rightGripAction;

        public bool IsTriggering => _trigger;
        public bool IsGripping => _grip;
        
        private bool _trigger;
        private bool _grip;
        
        
        private Hand _holdingHand;
        private bool _isHoldingHand;
        

        private void Update()
        {
            GetInput();
        }

        private void GetInput()
        {
            if (!_isHoldingHand)
            {
                _grip = false;
                _trigger = false;
                return;
            }

            var isLeft = _holdingHand.left;
            var gripValue = isLeft ? leftGripAction.action.ReadValue<float>() : rightGripAction.action.ReadValue<float>() ;
            var triggerValue = isLeft ? leftTriggerAction.action.ReadValue<float>() : rightTriggerAction.action.ReadValue<float>();
        
            _grip = gripValue > 0.3f;
            _trigger = triggerValue > 0.3f;
        }
        
        
        private void EnableActions()
        {
            leftTriggerAction.action.Enable();
            leftGripAction.action.Enable();
            rightTriggerAction.action.Enable();
            rightGripAction.action.Enable();
        }

        #region EVENTS
        private void OnGrab(Hand hand, Grabbable grabbable)
        {
            _holdingHand = hand;
            _isHoldingHand = true;
        }

        private void OnRelease(Hand hand, Grabbable grabbable)
        {
            _holdingHand = null;
            _isHoldingHand = false;
        }
        
        private void SubscribeEvents()
        {
            gunGrabbable.OnGrabEvent += OnGrab;
            gunGrabbable.OnReleaseEvent += OnRelease;
        }

        private void UnsuscribeEvents()
        {
            gunGrabbable.OnGrabEvent -= OnGrab;
            gunGrabbable.OnReleaseEvent -= OnRelease;
        }
        #endregion
        
        private void OnEnable()
        {
            EnableActions();
            SubscribeEvents();
        }

        private void OnDisable() => UnsuscribeEvents();

    }
}