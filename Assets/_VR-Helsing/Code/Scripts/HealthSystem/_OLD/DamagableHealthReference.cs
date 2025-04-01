using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _VanHelsingVR.Health
{
    [Serializable]
    public class DamagableHealthReference
    {
        [field: SerializeField] public Health HealthSystem { get; private set; }
    }
}
