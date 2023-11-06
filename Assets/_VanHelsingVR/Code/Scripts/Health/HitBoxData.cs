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
    public struct HitBoxData
    {
        public Vector4 Vector4Value { get; set; }
        public Vector3 Vector3Value { get; set; } 
        public Vector2 Vector2Value { get; set; } 
        public float FloatValue { get; set; } 
        public int IntValue { get; set; }
        public bool BoolValue { get; set; }
        
    }
}