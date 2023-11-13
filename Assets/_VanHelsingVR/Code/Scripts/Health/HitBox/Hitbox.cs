using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace _VanHelsingVR.Health
{
    // Es requerido que usen un collider y un rigidbody las hitbox para que se manden a llamar los eventos de fisicas
    // Ejemplo: "OnTriggerEnter" & "OnCollisionEnter"
    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public class Hitbox : MonoBehaviour
    {
        [SerializeField] private Collider hitBoxCollider;
        [SerializeField] private Rigidbody rb;

        [field: SerializeField] public DamageTeam DamageTeam { get; private set; }

        [SerializeField] private UnityEvent onTouchedHurtBox;
        [SerializeField] private UnityEvent onHit;

        public HitBoxData DataContainer => _dataContainer;
        private HitBoxData _dataContainer;

        private void Awake()
        {
            GetComponents();
        }

        //Obtiene los componentes solo si son nulos, si no son exactamente iguales
        private void GetComponents()
        {
            hitBoxCollider ??= GetComponent<Collider>();
            hitBoxCollider.isTrigger = true;
            
            rb ??= GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.isKinematic = true;
        }

        public void PopulateData(HitBoxData container) => _dataContainer = container;

        public void AddHitBoxData(HitBoxDataType dataType, HitBoxDataAttribute attribute)
        {
            _dataContainer.SetHitBoxData(dataType, attribute);
        }

        public void OnHit(Hurtbox hittedHurtBox)
        {
            if (hittedHurtBox == null) return;
            
            onHit?.Invoke();
        }

        public void OnHurtBoxTouched(Hurtbox touchedHurtBox)
        {
            if (touchedHurtBox == null) return;
            
            onTouchedHurtBox?.Invoke();
        }
        
        #if UNITY_EDITOR
        private void OnValidate()
        {
            GetComponents();
        }
        #endif
    }
}
