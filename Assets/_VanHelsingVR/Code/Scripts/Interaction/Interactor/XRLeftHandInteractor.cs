using _VanHelsingVR.Variables;
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
                if (isGrabbing) return false;

                return handInput.Reference.IsClosed;
            }
        }

        public void Grab(Grabbable grabbable)
        {
            isGrabbing.Value = true;
            Debug.Log("La mano izquierda agarro");
        }

        public void Ungrab(Grabbable grabbable)
        {
            Debug.Log("La mano izquierda dejo");
            isGrabbing.Value = false;
        }
    }
}
