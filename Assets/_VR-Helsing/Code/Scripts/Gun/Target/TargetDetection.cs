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

        private GunConfig _config => gunHandler.DataHandler.Config;
        private Crosshair _crossHair;
        private float _lastUpdateTime;

        private ITargetable _targetable;

        private void Start()
        {
            _crossHair = gunHandler.Crosshair;
        }

        private void Update()
        {
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

            if (hitCount <= 0) return;

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
                ChangeCrosshair(_targetable.TargetProfile);
            }//Hacer que cuando no, ponga el default
        }

        private void ChangeCrosshair(TargetProfile targetProfile)
        {
            
        }
    }
}