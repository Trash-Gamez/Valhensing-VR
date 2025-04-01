using System;
using System.Collections;
using System.Collections.Generic;
using _VR_Helsing.HealthSystem;
using UnityEngine;

namespace _VanHelsingVR.Health
{
    [Serializable]
    public class DamagableHealthReference
    {
        [field: SerializeField] public HealthSystem HealthSystem { get; private set; }
    }
}
