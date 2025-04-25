using UnityEngine;
using UnityEngine.InputSystem;

namespace _VanHelsingVR.Animation.Gun
{
    public class GunInput : MonoBehaviour
    {
        [SerializeField] private InputActionProperty leftTriggerAction;
        [SerializeField] private InputActionProperty leftGripAction;
        [SerializeField] private InputActionProperty rightTriggerAction;
        [SerializeField] private InputActionProperty rightGripAction;

        private bool trigger;
        private bool grip;
    }
}