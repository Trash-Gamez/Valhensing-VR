using System;
using _VanHelsingVR.Player;
using RacTools.Variables;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;


namespace _VanHelsingVR.Interaction
{
    public class XRLeftHandInteractor : UnityEngine.XR.Interaction.Toolkit.Interactors.XRDirectInteractor
    {
        [SerializeField] private HandInputReference handInput;
        [SerializeField, Required] private Variable<bool> isGrabbing;
        [SerializeField] private Collider punchCollider;

        protected override void Start()
        {
            isGrabbing.Value = false;
            base.Start();
        }

        private void FixedUpdate()
        {
            punchCollider.enabled = handInput.Hand.IsClosed;
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
