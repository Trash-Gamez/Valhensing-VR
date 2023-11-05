using System;
using UnityEngine;

namespace _VanHelsingVR.Health
{
    [RequireComponent(typeof(Collider))]
    public class Hitbox : MonoBehaviour
    {
        [SerializeField]
        private Collider hitBoxCollider;

        protected virtual void OnHit()
        {
            
        }
        
        
        #if UNITY_EDITOR
        private void OnValidate()
        {
            // Si no existe el componente collider, lo obtiene cada vez que se validen las propiedades del gameobject
            hitBoxCollider ??= GetComponent<Collider>();
        }
        #endif
    }
}
