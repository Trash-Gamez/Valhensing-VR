using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

using _VanHelsingVR.Instructions;
using _VanHelsingVR.Conditions;
using RacTools.Variables;

#if UNITY_EDITOR
using UnityEditor.Events;
#endif

namespace _VanHelsingVR.Health
{
    public class Health : MonoBehaviour
    {
        public int MaxHealth => maxHealth.Value;
        [Title("Health Config")] 
        [SerializeField] private VariableReference<int> maxHealth;

        public int CurrentHealth => currentHealth.Value;
        [SerializeField] private VariableReference<int> currentHealth;
        
        public int CurrentOverHealth => maxOverHeal.Value;
        [SerializeField] private VariableReference<int> maxOverHeal;
        public int OverHealValue => currentOverHeal.Value;
        [SerializeField] private VariableReference<int> currentOverHeal;
        

        [Space] 
        [SerializeField] private ConditionPool canBeDamaged;
        [SerializeField] private ConditionPool canBeHealed;
        [SerializeField] private ConditionPool canOverHeal;
        
        [Space]
        [SerializeField] private UnityEvent onDamage;
        [SerializeField] private UnityEvent onHeal;
        [SerializeField] private UnityEvent onOverHeal;
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
            if (!canBeDamaged) return;
            
            removedLife = OnDamage(removedLife);
            if (removedLife == 0) return;

            removedLife = OnDamagedOverHeal(removedLife);
            if (removedLife == 0) return;
            
            currentHealth.Value -= removedLife;
            onDamage?.Invoke();
            
            if(currentHealth.Value <= 0) Dead();
            
            currentHealth.Value = Mathf.Clamp(currentHealth.Value,0, maxHealth.Value);
        }

        /// <summary>
        /// Metodo usado para añadir vida, dependiendo "addedLife".
        /// Si "addedLife" es menor o igual a 0, no se curará nada
        /// </summary>
        /// <param name="addedLife">La vida que se añadira a este sistema de vida</param>
        public void Heal(int addedLife)
        {
            if (!canBeHealed) return;
            
            addedLife = OnHeal(addedLife);
            if (addedLife == 0) return;

            var addedHealth = currentHealth.Value + addedLife;

            currentHealth.Value += addedLife;
            onHeal.Invoke();
            currentHealth.Value = Mathf.Clamp(currentHealth.Value ,0, maxHealth.Value);

            if (addedHealth <= maxHealth.Value) return;
            
            //Over Heal Logic
            var overHealLife = addedHealth - maxHealth.Value;
            OverHeal(overHealLife);
        }

        private void OverHeal(int addedOverHeal)
        {
            if (!canOverHeal) return;

            addedOverHeal = OnOverHeal(addedOverHeal);
            if (addedOverHeal == 0) return;

            currentOverHeal.Value += addedOverHeal;
            onOverHeal?.Invoke();
            currentOverHeal.Value = Mathf.Clamp(currentOverHeal.Value ,0, maxOverHeal.Value);
            
            //TODO: Corutina para ir quitando el overheal
        }

        private bool _isHealthDead;
        private void Dead()
        {
            if (!OnDead()) return;
            if(_isHealthDead) return;
            
            _isHealthDead = true;
            
            currentHealth.Value = 0;
            onDead?.Invoke();
        }
        
        /// <summary>
        /// Este metodo se llama cuando el sistema de vida recibe daño,
        /// aqui se puede hacer cualquier alteracion a cuanto daño recibe un sistema de vida en especifico
        /// </summary>
        /// <param name="damagedValue">El valor de daño que recibe el jugador</param>
        /// <returns>Retorna el daño que se aplicará</returns>
        protected virtual int OnDamage(int damagedValue)
        {
            damagedValue = Mathf.Abs(damagedValue);
            
            return damagedValue;
        }
        
        protected virtual int OnDamagedOverHeal(int removedHealth)
        {
            //si el overheal es menor o igual a 0 se retorna la vida removida original, pues no se quita nada
            if (currentOverHeal.Value <= 0)
            {
                currentOverHeal.Value = 0;
                return removedHealth;
            }
            
            //Se le resta el valor de la vida removida y si el overheal es mayo o igual a 0, ya no se quita la vida del sistema normal
            currentOverHeal.Value -= removedHealth;
            if (currentOverHeal.Value >= 0) return 0;
            
            //Se obtiene el valor absoluto de overheal si es menor a 0, osea nos soltará el restante de vida que deberemos quitar
            //Se lo asignamos a la variable removedLife y asignamos a 0 el valor del overheal
            removedHealth = Mathf.Abs(currentOverHeal.Value);
            currentOverHeal.Value = 0;

            return removedHealth;
        }
        
        /// <summary>
        /// Este metodo se llama cuando el sistema de vida recibe sanacion,
        /// aqui se puede hacer cualquier alteracion a cuanta sanacion recibe un sistema de vida en especifico
        /// </summary>
        /// <param name="healValue">El valor de sanacion que recibe el jugador</param>
        /// <returns></returns>
        protected virtual int OnHeal(int healValue)
        {
            healValue = Mathf.Abs(healValue);

            return healValue;
        }

        protected virtual int OnOverHeal(int addedOverHeal)
        {
            addedOverHeal = Mathf.Abs(addedOverHeal);
            return addedOverHeal;
        }
        
        /// <summary>
        /// Este metodo se llama cuando el sistema de vida muere.
        /// Este metodo se usa para alterar la funcionalidad de la muerte en los sistemas de vida existentes.
        /// </summary>
        /// <returns>Retorna verdadero o falso si deseas que la muerte se realice</returns>
        protected virtual bool OnDead()
        {
            return currentHealth.Value <= 0;
        }

        #region Codigo Editor

        #if UNITY_EDITOR

        [ContextMenu("Health/Hurt")]
        public void EditorHurt()
        {
            Damage(1);
        }
        
        [ContextMenu("Health/Kill")]
        public void EditorKill()
        {
            Damage(999999);
        }
        
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
            
            UnityEventTools.AddFloatPersistentListener(onHeal, destroy.DestroySelf, 0);
        }
        #endif

        #endregion

    }
}