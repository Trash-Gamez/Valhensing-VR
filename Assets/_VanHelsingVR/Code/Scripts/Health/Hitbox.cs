using System;
using UnityEngine;

namespace _VanHelsingVR.Health
{
    // Es requerido que usen un collider y un rigidbody las hitbox para que se manden a llamar los eventos de fisicas
    // Ejemplo: "OnTriggerEnter" & "OnCollisionEnter"
    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public class Hitbox : MonoBehaviour
    {
        [SerializeField] private Collider hitBoxCollider;

        [SerializeField] private Rigidbody rigidbody;

        public HitBoxDataContainer Data => _data;
        private HitBoxDataContainer _data = default;

        public void PopulateData(HitBoxDataContainer container)
        {
            _data = container;
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
