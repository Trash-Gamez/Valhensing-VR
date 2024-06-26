using System;
using UnityEngine;

namespace _VanHelsingVR.Health
{
    //ASEGURATE QUE LA LAYER DEL OBJETO QUE TIENE EL PUNCHABLE SEA LAYER PUNCHABLE
    [RequireComponent(typeof(SpeedoMeter))]
    public class PunchableHitbox : Hitbox
    {
        [field: SerializeField] public SpeedoMeter SpeedoMeter { get; private set; }
        
        void Awake()
        {
            SpeedoMeter ??= GetComponent<SpeedoMeter>();
        }
    }
}