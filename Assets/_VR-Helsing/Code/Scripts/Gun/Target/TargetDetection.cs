using System;
using UnityEngine;

namespace _VR_Helsing.Gun.Target
{
    public class TargetDetection : MonoBehaviour
    {
        private static readonly RaycastHit[] _Hits = new RaycastHit[5];
        
        [Header("Gun Params")]
        [SerializeField] private GunHandler gunHandler;
        
        [Header("Update Params")]
        [SerializeField, Min(0)] private float updateFrequency;

        [Header("Detection Params")]
        [SerializeField] private LayerMask targetMask;

        [Header("Target Params")]
        [SerializeField] private TargetProfile noneProfile;
        [SerializeField] private TargetMovement targetMovement;

        private GunConfig _config => gunHandler.DataHandler.Config;
        private Crosshair _crossHair;
        private float _lastUpdateTime;

        private ITargetable _targetable;
        private ITargetable _lastTargetable;

        private void Start()
        {
            _crossHair = gunHandler.Crosshair;
        }

        private void Update()
        {
            //Cada que la resta del triempo activo menos "lastUpdateTime" sea mayor a la frecuencia de actualizacion, intentara detectar un objetivo
            if (Time.time - _lastUpdateTime >= updateFrequency)
            {
                DetectTarget();
                _lastUpdateTime = Time.time;
            }
        }

        private void DetectTarget()
        {
            bool hasDetectedTarget = false;
            var aimRay = new Ray(gunHandler.ShootPoint.position, gunHandler.ShootPoint.forward);
            var hitCount = Physics.SphereCastNonAlloc(aimRay, _config.BulletRadius, _Hits, _config.FireRange, targetMask);

            if (hitCount <= 0)
            {
                ResetCrosshair();
                return;
            }

            for (int i = 0; i < hitCount; i++)
            {
                if (_Hits[i].transform.TryGetComponent(out _targetable) 
                    && _targetable.IsTargetable)
                {
                    hasDetectedTarget = true;
                    break;
                }
            }

            if (hasDetectedTarget)
            {
                _lastTargetable = _targetable;
                SetCrosshairOnTarget(_lastTargetable);
            }
            else
            {
                ResetCrosshair();
            }
        }

        private void SetCrosshairOnTarget(ITargetable targetable)
        {
            ChangeCrosshair(targetable.TargetProfile);
            
            // Cast from "IITargetable" -> "TargetableObject" to get transform from object
            if(targetable is TargetableObject targetableObject)
                targetMovement.SetTargetable(targetableObject);
        }

        private void ResetCrosshair()
        {
            if (_lastTargetable == null) return;
            
            ChangeCrosshair(noneProfile);
            _lastTargetable = null;
            
            //Nullifies the target on move to set default movement, no pointing in any target
            targetMovement.SetTargetable(null);
        }

        private void ChangeCrosshair(TargetProfile targetProfile)
        {
            _crossHair.SetTargetProfile(targetProfile);
        }
    }
}