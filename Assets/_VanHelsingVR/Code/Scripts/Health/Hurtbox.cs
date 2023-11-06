using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace _VanHelsingVR.Health
{
    
    [RequireComponent(typeof(Collider))]
    public class Hurtbox : MonoBehaviour
    {
        [SerializeField] private Collider hurtBoxCollider;

        [SerializeField] private LayerMask hurtLayer;
        
        [SerializeField] private DamagableHealthReference healthReference;

        [SerializeField] private UnityEvent<int> onHit;
        
        protected virtual void OnHit(Hitbox hitbox)
        {
            if (hitbox == null) return;
            
            //Se obtiene el valor Damage y su tipo de dato entero, para saber cuanto daño se realizó
            var damageDealed = hitbox.Data.GetHitBoxData(HitBoxDataType.Damage).IntValue;
            
            if(healthReference != null)
                healthReference.HealthSystem.Damage(damageDealed);
            
            onHit?.Invoke(damageDealed);
        }

        public virtual void OnHitScan()
        {
            if(healthReference != null)
                healthReference.HealthSystem.Damage(1);
        }

        private void OnForcedHit(int damageDealed = 1)
        {
            if(healthReference != null)
                healthReference.HealthSystem.Damage(damageDealed);
        }

        private void OnTriggerEnter(Collider other)
        {
            var otherLayer = other.gameObject.layer;
            
            /*
             * Esto se hace para saber si las layerMask asignadas a "hurtLayer"
             * son compatibles con la layerMask del objeto con el que se activaron las fisicas
            */
            
            if (hurtLayer != (hurtLayer | 1 << otherLayer)) return;
            Debug.Log(string.Format("La layer *{0}* del objeto *{1}* es compatible con la layer *{2}* del objeto *{3}*",
            hurtLayer, name, otherLayer, other.gameObject.name));
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
