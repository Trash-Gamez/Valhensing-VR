using UnityEngine;
using UnityEngine.Serialization;

namespace _VanHelsingVR.Health
{
    // Es requerido que usen un collider y un rigidbody las hurtbox para que se manden a llamar los eventos de fisicas
    // Ejemplo: "OnTriggerEnter" & "OnCollisionEnter"
    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public class Hurtbox : MonoBehaviour
    {
        [FormerlySerializedAs("hitBoxCollider")] [SerializeField]
        private Collider hurtBoxCollider;

        
        
        #if UNITY_EDITOR
        private void OnValidate()
        {
            // Si no existe el componente collider, lo obtiene cada vez que se validen las propiedades del gameobject
            hurtBoxCollider ??= GetComponent<Collider>();
        }
        #endif
    }
}
