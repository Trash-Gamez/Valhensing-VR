using UnityEngine;
using UnityEngine.Events;

namespace _VanHelsingVR.Health
{
    [RequireComponent(typeof(Collider))]
    public class Hurtbox : MonoBehaviour
    {
        [SerializeField] private Collider hurtBoxCollider;

        [SerializeField] private LayerMask hurtLayer;
        [SerializeField] private DamagableHealthReference healthReference;
        [SerializeField] private DamageTeam damageTeam;
        
        [Space]
        [SerializeField] private UnityEvent<int> onHit;
        

        protected virtual void Awake()
        {
            hurtBoxCollider ??= GetComponent<Collider>();
        }

        /// <summary>
        /// Este metodo sucede justo antes de Sucede un Hit
        /// Sobreescribe este metodo si deseas hacer tu propia validacion para acertar el golpe
        /// </summary>
        /// <returns>Si procede el golpe o no</returns>
        protected virtual bool OnBeforeHit(Hitbox _) => true;

        private void OnHit(Hitbox hitbox)
        {
            if (hitbox == null) return;
            
            if (!OnBeforeHit(hitbox)) return;
            
            //Se obtiene el valor Damage y su tipo de dato entero, para saber cuanto daño se realizó
            var damageDealed = hitbox.Data.GetHitBoxData(HitBoxDataType.Damage).IntValue;
            
            if(healthReference != null)
                healthReference.HealthSystem.Damage(damageDealed);
            
            onHit?.Invoke(damageDealed);
        }
        
        /// <summary>
        /// Este metodo sucede justo antes de Sucede un HitScan
        /// Sobreescribe este metodo si deseas hacer tu propia validacion para acertar el rayo
        /// </summary>
        /// <returns>Si procede el rayo o no</returns>
        protected virtual bool OnBeforeHitScan() => true;

        public virtual void OnHitScan(int damaged = 1)
        {
            if (!OnBeforeHitScan()) return;
            
            if (healthReference != null)
                healthReference.HealthSystem.Damage(damaged);
            
            onHit?.Invoke(damaged);
        }

        protected void OnForcedHit(int damageDealed = 1)
        {
            if (healthReference == null) return;
            
            healthReference.HealthSystem.Damage(damageDealed);
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            var otherLayer = other.gameObject.layer;
            
            /*
             * Esto se hace para saber si las layerMask asignadas a "hurtLayer"
             * son compatibles con la layerMask del objeto con el que se activaron las fisicas
            */
            if (hurtLayer != (hurtLayer | 1 << otherLayer)) return;
            
            //Success!!!
            Debug.Log(string.Format("La layer *{0}* del objeto *{1}* es compatible con la layer *{2}* del objeto *{3}*",  hurtLayer, name, otherLayer, other.gameObject.name));

            if (!other.TryGetComponent(out Hitbox hitBox)) return;
            
            OnHit(hitBox);
        }

        #region Editor Methods

        #if UNITY_EDITOR
        private void OnValidate()
        {
            // Si no existe el componente collider, lo obtiene cada vez que se validen las propiedades del gameobject
            hurtBoxCollider ??= GetComponent<Collider>();
        }
        
        [ContextMenu("Force Hit")]
        public void ForceHandleDamage()
        {
            OnForcedHit();
        }
    
        #endif

        #endregion
    }
}
