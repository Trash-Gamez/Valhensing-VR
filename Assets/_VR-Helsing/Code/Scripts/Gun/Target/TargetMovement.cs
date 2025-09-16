using System;
using System.Collections.Generic;
using MEC;
using UnityEngine;

namespace _VR_Helsing.Gun.Target
{
    public class TargetMovement : MonoBehaviour
    {
        [SerializeField] private Crosshair crosshair;
        [Tooltip("Ensure this component has the 'Z axis' in the direction the gun is pointing ")]
        [SerializeField] private Transform gunTip;

        [Tooltip("Usually this is the same as 'GunConfig.FireRange'")]
        [SerializeField] private float targetMaxDistance;

        private TargetableObject _targetable;
        private CoroutineHandle _movementCor;

        private void UpdateTargetCoroutine()
        {
            Timing.KillCoroutines(_movementCor);
            
            _movementCor = Timing.RunCoroutine(_targetable
                ? FollowTargetCor()
                : FollowMaxDistanceCor()
            );
        }

        public void SetTargetable(TargetableObject newTargetable)
        {
            _targetable = newTargetable;

            UpdateTargetCoroutine();
        }
        
        /// <summary>
        /// Listener Methos from event "<c>crosshair.OnCrosshairSetActive</c>"
        /// </summary>
        /// <param name="isActive">Active Value From Crosshair</param>
        private void OnCrosshairSetActive(bool isActive)
        {
            if (!isActive)
                Timing.KillCoroutines(_movementCor);
            else
                UpdateTargetCoroutine();
        }

        /// <summary>
        /// Follow the current <see cref="_targetable"/>
        /// </summary>
        /// <returns>Coroutine</returns>
        private IEnumerator<float> FollowTargetCor()
        {
            while (_targetable)
            {
                var distance = Vector3.Distance(gunTip.position, _targetable.transform.position);
                Vector3 defaultPos = gunTip.position + gunTip.forward * distance;
                crosshair.transform.position = defaultPos;
                crosshair.transform.rotation = Quaternion.LookRotation(defaultPos - Camera.main.transform.position);
                yield return Timing.WaitForOneFrame;
            }
        }
        
        /*
         * PEGA ESTE CODIGO EN LA FUNCION DE ARRIBA SI SOLO QUIERE SE QUEDE PEGADO AL TARGET:
         * 
         var crosshairTransform = crosshair.transform;
                crosshairTransform.position = _targetable.CenterTarget 
                    ? _targetable.CenterTarget.position 
                    : _targetable.transform.position;
                
                crosshairTransform.rotation = 
                    Quaternion.LookRotation(crosshair.transform.position - Camera.main.transform.position);
                yield return Timing.WaitForOneFrame;
         */
        
        /// <summary>
        /// Follow The Max Distance set in <param name="targetMaxDistance">Target Max Distance</param>
        /// </summary>
        /// <returns>Couroutine</returns>
        private IEnumerator<float> FollowMaxDistanceCor()
        {
            while (true)
            {
                Vector3 defaultPos = gunTip.position + gunTip.forward * targetMaxDistance;
                crosshair.transform.position = defaultPos;
                crosshair.transform.rotation = Quaternion.LookRotation(defaultPos - Camera.main.transform.position);
                yield return Timing.WaitForOneFrame;
            }
        }

        private void OnEnable()
        {
            crosshair.OnCrosshairSetActive += OnCrosshairSetActive;
        }


        private void OnDisable()
        {
            crosshair.OnCrosshairSetActive -= OnCrosshairSetActive;
        }

        private void OnDestroy()
        {
            Timing.KillCoroutines(_movementCor);
            //Kill All Coroutines
        }
    }
}