using Sirenix.OdinInspector;
using UnityEngine;

namespace _VanHelsingVR.Health
{
    //Los tipos de datos que hay en cada hurtbox
    public enum HitBoxDataType
    {
        Damage,
        KnockBack,
        Stun
    }
    
    [System.Serializable]
    public struct HitBoxDataAttribute
    {
        [field: SerializeField] public Vector4 Vector4Value { get; set; }
        [field: SerializeField] public Vector3 Vector3Value { get; set; } 
        [field: SerializeField] public Vector2 Vector2Value { get; set; } 
        [field: SerializeField] public float FloatValue { get; set; } 
        [field: SerializeField] public int IntValue { get; set; }
        [field: SerializeField] public bool BoolValue { get; set; }
        
    }
}