using UnityEngine;

namespace _VanHelsingVR.Health
{
    //Los tipos de datos que hay en cada hurtbox
    public enum HurtBoxDataType
    {
        Damage,
        Knockback
    }
    
    public class HurtBoxData
    {
        public virtual Vector4 Vector4Value { get; set; } = Vector4.zero;
        public virtual Vector3 Vector3Value { get; set; } = Vector3.zero;
        public virtual Vector2 Vector2Value { get; set; } = Vector2.zero;
        public virtual float FloatValue { get; set; } = 0.0f;
        public virtual int IntValue { get; set; } = 0;
        public virtual bool BoolValue { get; set; } = false;
    }
    
    
}