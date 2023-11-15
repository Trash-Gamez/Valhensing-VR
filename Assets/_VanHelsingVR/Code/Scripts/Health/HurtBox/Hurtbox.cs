using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

using _VanHelsingVR.Conditions;
using _VanHelsingVR.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace _VanHelsingVR.Health
{
    [RequireComponent(typeof(Collider))]
    public class Hurtbox : MonoBehaviour
    {
        [SerializeField] protected Collider hurtBoxCollider;

        [SerializeField] protected LayerMask hurtLayer;
        [Title("Team")] 
        [field: SerializeField] public DamageTeam HurtboxTeam { get; private set; }
        [SerializeField] protected ConditionPool friendlyFire;
        
        [Title("Health")] 
        [SerializeField] protected DamagableHealthReference healthReference;

        [Title("Inmunity")] 
        [SerializeField] private InmunityType inmunityType;
        [SerializeField, Min(0)] private int inmunityFrames;
        private static readonly HashSet<Transform> _hurtBoxesInmune = new HashSet<Transform>();
        
        [Space]
        [SerializeField] protected UnityEvent<int> onHit;

        private enum InmunityType
        {
            HurtBox, 
            All
        }
        
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

        protected void OnHit(Hitbox hitbox)
        {
            if(_invulneravilityCor != null || _hurtBoxesInmune.Contains(transform.root)) return;
            if (hitbox == null) return;
            if (hitbox.DamageTeam == HurtboxTeam && !friendlyFire) return;
            
            hitbox.OnHurtBoxTouched(this);
            if (!OnBeforeHit(hitbox)) return;
            hitbox.OnHit(this);
            
            //Se obtiene el valor Damage y su tipo de dato entero, para saber cuanto daño se realizó
            var damageDealed = hitbox.DataContainer.GetHitBoxData(HitBoxDataType.Damage).IntValue;

            if (healthReference.HealthSystem != null)
            {
                healthReference.HealthSystem.Damage(damageDealed);                
            }
            

            onHit?.Invoke(damageDealed);
            _invulneravilityCor = StartCoroutine(InvulnerabilityCor());
        }

        
        /// <summary>
        /// Este metodo sucede justo antes de Sucede un HitScan
        /// Sobreescribe este metodo si deseas hacer tu propia validacion para acertar el rayo
        /// </summary>
        /// <returns>Si procede el rayo o no</returns>
        protected virtual bool OnBeforeHitScan() => true;

        public void OnHitScan(int damaged = 1)
        {
            if (!OnBeforeHitScan()) return;
            
            if (healthReference != null) {
                healthReference.HealthSystem.Damage(damaged);
            }
            
            onHit?.Invoke(damaged);
        }

        protected virtual bool OnBeforePunch(PunchableHitbox _) => true;

        protected virtual void OnPunch(PunchableHitbox punchable)
        {
            if (!OnBeforePunch(punchable)) return;
            
            OnHit(punchable);
        }

        protected void OnForcedHit(int damageDealed = 1)
        {
            if (healthReference == null) return;
            
            healthReference.HealthSystem.Damage(damageDealed);
        }

        //Fuerzxa la muerte del que contenga esto
        public void ForceDeath()
        {
            if (healthReference == null) return;
            healthReference.HealthSystem.Damage(999999999);
        }

        private Coroutine _invulneravilityCor;
        protected IEnumerator InvulnerabilityCor()
        {
            if (inmunityFrames == 0)
            {
                _invulneravilityCor = null;
                yield break;
            }

            if (inmunityType == InmunityType.All)
            {
                _hurtBoxesInmune.Add(transform.root);
            }
            else
            {
                for (int i = 1; i < inmunityFrames; i++)
                {
                    yield return new WaitForFixedUpdate();
                }
            }

            _hurtBoxesInmune.Remove(transform.root);
            _invulneravilityCor = null;
        }
        
        protected virtual void OnTriggerEnter(Collider other)
        {
            var otherLayer = other.gameObject.layer;
            
            /*
             * Esto se hace para saber si las layerMask asignadas a "hurtLayer"
             * son compatibles con la layerMask del objeto con el que se activaron las fisicas
            */
            if (hurtLayer != (hurtLayer | 1 << otherLayer)) return;
            

            if (!other.TryGetComponent(out Hitbox hitBox)) return;
            
            //Selecciona si es punch o hit
            switch (hitBox)
            {
                case PunchableHitbox punchable:
                    OnPunch(punchable);
                    break;
                case Hitbox hitbox:
                    OnHit(hitbox);
                    break;
            }
        }

        #region Editor Methods

#if UNITY_EDITOR
        private void OnValidate()
        {
            // Si no existe el componente collider, lo obtiene cada vez que se validen las propiedades del gameobject
            hurtBoxCollider ??= GetComponent<Collider>();
            if(hurtBoxCollider != null)
                hurtBoxCollider.isTrigger = true;
        }
        
        [ContextMenu("Force Hit")]
        public void ForceHandleDamage()
        {
            OnForcedHit();
        }
        
        [ContextMenu("Force Hit", true)]
        public bool ValidateForceHandleDamage()
        {
            return Application.isPlaying || EditorApplication.isPlaying;
        }
    
#endif

        #endregion
    }
}
