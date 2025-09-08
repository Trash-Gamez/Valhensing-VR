using System;
using UnityEngine;

namespace _VR_Helsing.Gun.Target
{
    public class TargetDetection : MonoBehaviour
    {
        [Header("Update Params")]
        [SerializeField, Min(0)] private float updateFrequency;
        
        private float _lastUpdateTime;
        private void Update()
        {
            if (Time.time - _lastUpdateTime >= updateFrequency)
            {
                //DetectTarget();
                _lastUpdateTime = Time.time;
            }
        }
    }
}