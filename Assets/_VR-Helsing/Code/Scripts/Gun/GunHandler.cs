using System;
using System.Collections.Generic;
using UnityEngine;
using Autohand;

using _VR_Helsing.Gun.Target;

namespace _VR_Helsing.Gun
{
    /// <summary>
    /// The main class that handles all Gun Logic
    /// </summary>
    public class GunHandler : MonoBehaviour
    {
        [Header("Gun Settings")] 
        [SerializeField] private GunDataHandler dataHandler;

        [Header("Grabbable")] 
        [SerializeField] private Grabbable grabbable;
        
        [Header("Gun Properties")]
        [SerializeField] private Rigidbody gunRigidbody;
        [SerializeField] private Animator gunAnimator;
        [SerializeField] private Transform shootPoint;
        
        [Header("Gun Input")]
        [SerializeField] private GunInput gunInput;

        [Header("Crosshair")] 
        [SerializeField] private Crosshair crossHair;

        #region GETTERS & SETTERS
        
        public GunDataHandler DataHandler => dataHandler;

        public Grabbable Grabbable => grabbable;
        public Crosshair Crosshair => crossHair;
        public Transform ShootPoint => shootPoint;
        public bool IsEquipped => _isEquipped;
        #endregion

        private bool _isEquipped;


        
        
        public void SetEquipped(bool value)
        {
            _isEquipped = value;
            crossHair.SetCrosshairActive(value);
        }
    }
}
