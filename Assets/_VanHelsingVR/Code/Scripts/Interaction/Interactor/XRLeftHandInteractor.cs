using _VanHelsingVR.Player;
using RacTools.Variables;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;

namespace _VanHelsingVR.Interaction
{
    public class XRLeftHandInteractor : XRDirectInteractor
    {
        [SerializeField] private HandInputReference handInput;
        [SerializeField, Required] private Variable<bool> isGrabbing;

        protected override void Start()
        {
            isGrabbing.Value = false;
            base.Start();
        }

        public bool CanPunch
        {
            get
            {
                if (isGrabbing.Value) return false;
                return handInput.Hand.IsClosed;
            }
        }

        public void Grab(Grabbable grabbable)
        {
            isGrabbing.Value = true;
        }

        public void Ungrab(Grabbable grabbable)
        {
            isGrabbing.Value = false;
        }
    }
}
