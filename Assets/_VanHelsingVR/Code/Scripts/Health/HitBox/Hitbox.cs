using UnityEngine;
using UnityEngine.Events;

namespace _VanHelsingVR.Health
{
    // Es requerido que usen un collider y un rigidbody las hitbox para que se manden a llamar los eventos de fisicas
    // Ejemplo: "OnTriggerEnter" & "OnCollisionEnter"
    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public class Hitbox : MonoBehaviour
    {
        [SerializeField] private Collider hitBoxCollider;
        [SerializeField] private Rigidbody rigidbody;

        [field: SerializeField] public DamageTeam DamageTeam { get; private set; }

        [SerializeField] private UnityEvent onHit;

        public HitBoxData Data => _data;
        private HitBoxData _data = default;

        private void Awake()
        {
            //Obtiene los componentes solo si son nulos, si no son exactamente iguales
            hitBoxCollider ??= GetComponent<Collider>();
            rigidbody ??= GetComponent<Rigidbody>();
        }
        
        public void PopulateData(HitBoxData container) => _data = container;

        public void OnHit(Hurtbox hittedHurtBox)
        {
            if (hittedHurtBox == null) return;
            
            onHit?.Invoke();
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
