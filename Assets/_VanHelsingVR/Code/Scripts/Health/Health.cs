using _VanHelsingVR.Conditions;
using UnityEngine;
using RacTools.Variables;
using Sirenix.OdinInspector;
using UnityEngine.Events;

#if UNITY_EDITOR
using _VanHelsingVR.Instructions;
using UnityEditor.Events;
#endif

namespace _VanHelsingVR.Health
{
    public class Health : MonoBehaviour
    {
        [Title("Health Config")] [field: SerializeField]
        private VariableReference<int> maxHealth;

        public int CurrentHealth => currentHealth.Value;
        [SerializeField] private VariableReference<int> currentHealth;

        public int OverHealValue => overHealValue.Value;
        [SerializeField] private VariableReference<int> overHealValue;
        

        [Space] 
        [SerializeField] private ConditionPool canBeDamaged;
        [SerializeField] private ConditionPool canBeHealed;
        [SerializeField] private ConditionPool canOverHeal;
        
        [SerializeField] private UnityEvent onDamage;
        [SerializeField] private UnityEvent onRecoverHealth;
        [SerializeField] private UnityEvent onDead;


        private void Start()
        {
            SetInitialHealth();
            
        }

        protected virtual void SetInitialHealth()
        {
            currentHealth.Value = maxHealth.Value;
        }
        
        /// <summary>
        /// Metodo usado para quitar vida, dependiendo "addedLife".
        /// Si "addedLife" es mayor o igual a 0, no se curará nada
        /// </summary>
        /// <param name="removedLife">La vida que se removerá de este sistema de vida</param>
        public void Damage(int removedLife)
        {
            if (removedLife == 0) return;
            if (!canBeDamaged) return;
            
            removedLife = Mathf.Abs(removedLife);
            
            OnDamage(removedLife);
            
            currentHealth.Value = Mathf.Clamp(currentHealth.Value - removedLife,0, maxHealth.Value);
        }
        
        /// <summary>
        /// Metodo usado para añadir vida, dependiendo "addedLife".
        /// Si "addedLife" es menor o igual a 0, no se curará nada
        /// </summary>
        /// <param name="addedLife">La vida que se añadira a este sistema de vida</param>
        public void Heal(int addedLife)
        {
            if (addedLife == 0) return;
            if (!canBeHealed) return;
            
            addedLife = Mathf.Abs(addedLife);
            
            OnHeal(addedLife);
            currentHealth.Value = Mathf.Clamp( currentHealth.Value + addedLife ,0, maxHealth.Value);
        }
        
        /// <summary>
        /// Este metodo se llama cuando el sistema de vida recibe daño,
        /// aqui se puede hacer cualquier alteracion a cuanto daño recibe un sistema de vida en especifico
        /// </summary>
        /// <param name="damagedValue">El valor de daño que recibe el jugador</param>
        protected virtual void OnDamage(int damagedValue)
        {
            if (damagedValue == 0) return;
            
            damagedValue = Mathf.Abs(damagedValue);
            
            var damaged = currentHealth.Value - damagedValue;

            if (damaged <= 0)
            {
                OnDead();
                return;
            }

            currentHealth.Value = damaged;
        }
        
        /// <summary>
        /// Este metodo se llama cuando el sistema de vida recibe sanacion,
        /// aqui se puede hacer cualquier alteracion a cuanta sanacion recibe un sistema de vida en especifico
        /// </summary>
        /// <param name="damagedValue">El valor de daño que recibe el jugador</param>
        protected virtual void OnHeal(int healValue)
        {
            if (healValue == 0) return;

            healValue = Mathf.Abs(healValue);
            
            currentHealth.Value += healValue;
            onRecoverHealth.Invoke();
        }

        protected virtual void OnOverHeal()
        {
            
        }
        
        /// <summary>
        /// Este metodo se llama cuando el sistema de vida muere.
        /// Este metodo se usa para alterar la funcionalidad de la muerte en los sistemas de vida existentes.
        /// </summary>
        protected virtual void OnDead()
        {
            currentHealth.Value = 0;
            onDead.Invoke();
        }

        #region Codigo Editor

        #if UNITY_EDITOR
        [ContextMenu("DestroySelf/On Dead")]
        public void AddDestroyInstructionOnDead()
        {
            DestroyInstruction destroy;
            if (!TryGetComponent(out destroy))
            {
                destroy = gameObject.AddComponent<DestroyInstruction>();
            }
            UnityEventTools.AddFloatPersistentListener(onDead, destroy.DestroySelf, 0);
        }
        
        [ContextMenu("DestroySelf/On Damage")]
        public void AddDestroyInstructionOnDamage()
        {
            DestroyInstruction destroy;
            if (!TryGetComponent(out destroy))
            {
                destroy = gameObject.AddComponent<DestroyInstruction>();
            }
            
            UnityEventTools.AddFloatPersistentListener(onDamage, destroy.DestroySelf, 0);
        }
        
        [ContextMenu("DestroySelf/On Recover Health")]
        public void AddDestroyInstructionOnRecoverHealth()
        {
            DestroyInstruction destroy;
            if (!TryGetComponent(out destroy))
            {
                destroy = gameObject.AddComponent<DestroyInstruction>();
            }
            
            UnityEventTools.AddFloatPersistentListener(onRecoverHealth, destroy.DestroySelf, 0);
        }
        #endif

        #endregion

    }
}