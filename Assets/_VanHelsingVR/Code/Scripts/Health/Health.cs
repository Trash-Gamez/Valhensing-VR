using UnityEngine;
using _VanHelsingVR.Variables;
using _VanHelsingVR.Events;
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

        [Space] [SerializeField] private ConditionPool canBeDamaged;
        [SerializeField] private ConditionPool canBeHealed;
        
        [SerializeField] private UnityEvent onDamage;
        [SerializeField] private UnityEvent onRecoverHealth;
        [SerializeField] private UnityEvent onDead;
        public int health{get; private set;}

        private void Awake()
        {
            health = maxHealth.Value;
        }

        public void AddHealth(int addedLife)
        {
            if (addedLife == 0) return;

            if(addedLife < 0 && canBeDamaged.CanDo)
            {
                HandleDamage(addedLife);
            }

            if (addedLife > 0 && canBeDamaged)
            {
                HandleDamage(addedLife);
            }

            health = Mathf.Clamp(addedLife + health,0, maxHealth.Value);
        }

        protected virtual void HandleDamage(int damagedValue)
        {
            var damaged = health + damagedValue;

            if (damaged <= 0)
            {
                //TODO: milagro por parte del jugador, salvarse
                HandleDead();
            }
        }

        protected virtual void HandleHeal(int healValue)
        {
            onRecoverHealth.Invoke();
        }

        protected virtual void HandleDead()
        {
            onDead.Invoke();
        }
        
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
    }
}